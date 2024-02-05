using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    [UpdateBefore(typeof(PlayerCollisionDetectionSystem))]
    public partial struct EnemyCollisionDetectionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EnemyHealthComponent>();
            state.RequireForUpdate<EnemyDamagerComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            var enemyHealthLookup = SystemAPI.GetComponentLookup<EnemyHealthComponent>();
            var enemyDamagerLookup = SystemAPI.GetComponentLookup<EnemyDamagerComponent>(true);
            state.Dependency = new EnemyCollisionDetectionJob
            {
                DeltaTime = deltaTime,
                ECB = ecb,
                EnemyHealthLookup = enemyHealthLookup,
                EnemyDamagerLookup = enemyDamagerLookup
            }.Schedule(simulationSingleton, state.Dependency);
        }
    }
    
    public struct EnemyCollisionDetectionJob : ITriggerEventsJob
    {
        [ReadOnly] public float DeltaTime;
        public EntityCommandBuffer ECB;
        public ComponentLookup<EnemyHealthComponent> EnemyHealthLookup;
        [ReadOnly] public ComponentLookup<EnemyDamagerComponent> EnemyDamagerLookup;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log($"Trigger event: {triggerEvent.EntityA} and {triggerEvent.EntityB}");
            
            bool isEnemyEntityA = EnemyHealthLookup.HasComponent(triggerEvent.EntityA);
            bool isEnemyEntityB = EnemyHealthLookup.HasComponent(triggerEvent.EntityB);
            if (!isEnemyEntityA && !isEnemyEntityB)
            {
                Debug.LogError($"Trigger event after two non-player entities collided.");
                return;
            }

            Entity enemyEntity = triggerEvent.EntityA;
            Entity damagerEntity = triggerEvent.EntityB;
            if (isEnemyEntityB)
            {
                enemyEntity = triggerEvent.EntityB;
                damagerEntity = triggerEvent.EntityA;
            }
            
            if (!EnemyDamagerLookup.HasComponent(damagerEntity))
            {
                Debug.LogWarning($"Trigger event after enemy collided with non-damager entity.");   //TODO - remove when considered appropriate
                return;
            }

            var enemyDamagerComponent = EnemyDamagerLookup[damagerEntity];
            EnemyHealthLookup.GetRefRW(enemyEntity).ValueRW.Value -= enemyDamagerComponent.DamagePerSecond * DeltaTime;
        }
    }
}