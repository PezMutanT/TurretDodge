﻿using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial struct PlayerFiringSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var playerFiringAspect in SystemAPI.Query<PlayerFiringAspect>())
            {
                playerFiringAspect.FireTimer -= deltaTime;
                if (playerFiringAspect.FireTimer <= 0f)
                {
                    playerFiringAspect.FireTimer = playerFiringAspect.FireRate;
                    
                    var newProjectileEntity = ecb.Instantiate(playerFiringAspect.ProjectilePrefab);
                    
                    ecb.SetComponent(
                        newProjectileEntity,
                        LocalTransform.FromPosition(playerFiringAspect.FirePosition));
                    
                    ecb.AddComponent(
                        newProjectileEntity,
                        new ProjectileMovementComponent
                        {
                            Direction = math.forward(),     //TODO
                            Speed = 10f,
                            Lifetime = 1f
                        });
                    
                    ecb.AddComponent(newProjectileEntity,
                        new EnemyDamagerComponent
                        {
                            DamagePerSecond = playerFiringAspect.DamagePerSecond
                        });
                }
            }
        }
    }
}