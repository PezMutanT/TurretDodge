using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct PlayerCollisionDetectionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PlayerHealthComponent>();
            state.RequireForUpdate<PlayerDamagerComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            var playerHealthLookup = SystemAPI.GetComponentLookup<PlayerHealthComponent>(false);
            var playerDamagerLookup = SystemAPI.GetComponentLookup<PlayerDamagerComponent>(true);
            state.Dependency = new PlayerCollisionDetectionJob
            {
                ECB = ecb,
                PlayerHealthLookup = playerHealthLookup,
                PlayerDamagerLookup = playerDamagerLookup
            }.Schedule(simulationSingleton, state.Dependency);
        }
    }
    
    public struct PlayerCollisionDetectionJob : ITriggerEventsJob
    {
        public EntityCommandBuffer ECB;
        public ComponentLookup<PlayerHealthComponent> PlayerHealthLookup;
        [ReadOnly] public ComponentLookup<PlayerDamagerComponent> PlayerDamagerLookup;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log($"Trigger event: {triggerEvent.EntityA} and {triggerEvent.EntityB}");
            
            bool isPlayerEntityA = PlayerHealthLookup.HasComponent(triggerEvent.EntityA);
            bool isPlayerEntityB = PlayerHealthLookup.HasComponent(triggerEvent.EntityB);
            if (!isPlayerEntityA && !isPlayerEntityB)
            {
                //Debug.LogError($"Trigger event after two non-player entities collided.");
                return;
            }

            Entity playerEntity = triggerEvent.EntityA;
            Entity damagerEntity = triggerEvent.EntityB;
            if (isPlayerEntityB)
            {
                playerEntity = triggerEvent.EntityB;
                damagerEntity = triggerEvent.EntityA;
            }
            
            if (!PlayerDamagerLookup.HasComponent(damagerEntity))
            {
                Debug.LogWarning($"Trigger event after player collided with non-damager entity.");   //TODO - remove when considered appropriate
                return;
            }

            var playerDamagerComponent = PlayerDamagerLookup[damagerEntity];
            PlayerHealthLookup.GetRefRW(playerEntity).ValueRW.Value -= playerDamagerComponent.DamageOnHit;

            /*ECB.SetComponent(playerEntity, new PlayerHealthComponent
            {
                Value = playerHealth.Value - playerDamagerComponent.DamageOnHit
            });*/
                
            ECB.DestroyEntity(damagerEntity);
        }
    }
}