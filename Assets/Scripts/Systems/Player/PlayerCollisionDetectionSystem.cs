using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    //[UpdateInGroup(typeof(LateSimulationSystemGroup))]
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
            var aa = new PlayerCollisionDetectionJob
            {
                ECB = ecb,
                PlayerHealthLookup = playerHealthLookup,
                PlayerDamagerLookup = playerDamagerLookup
            }.Schedule(simulationSingleton, state.Dependency);
            
            aa.Complete();
            
            
            foreach (var a in simulationSingleton.AsSimulation().CollisionEvents)
            {
                Debug.Log($"Collision event");
            }
        }
    }
    
        public struct PlayerCollisionDetectionJob : ICollisionEventsJob
        {
            public EntityCommandBuffer ECB;
            public ComponentLookup<PlayerHealthComponent> PlayerHealthLookup;
            [ReadOnly] public ComponentLookup<PlayerDamagerComponent> PlayerDamagerLookup;
            
            public void Execute(CollisionEvent collisionEvent)
            {
                Debug.Log($"Trigger event: {collisionEvent.EntityA} and {collisionEvent.EntityB}");
                
                bool isPlayerEntityA = PlayerHealthLookup.HasComponent(collisionEvent.EntityA);
                bool isPlayerEntityB = PlayerHealthLookup.HasComponent(collisionEvent.EntityB);
                if (!isPlayerEntityA && !isPlayerEntityB)
                {
                    Debug.LogError($"Trigger event after two non-player entities collided.");
                    return;
                }

                Entity playerEntity = collisionEvent.EntityA;
                Entity damagerEntity = collisionEvent.EntityB;
                if (isPlayerEntityB)
                {
                    playerEntity = collisionEvent.EntityB;
                    damagerEntity = collisionEvent.EntityA;
                }
                
                if (!PlayerDamagerLookup.HasComponent(damagerEntity))
                {
                    Debug.LogWarning($"Trigger event after player collided with non-damageable entity.");   //TODO - remove when considered appropriate
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