using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial struct ProjectileSpawningSystem : ISystem
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
            foreach (var projectileSpawner in SystemAPI.Query<ProjectileSpawningAspect>())
            {
                projectileSpawner.SpawnProjectileTimer -= deltaTime;
                if (projectileSpawner.IsTimeToSpawnProjectile)
                {
                    projectileSpawner.SpawnProjectileTimer = projectileSpawner.SpawnProjectileFrequency;

                    var newProjectileEntity = ecb.Instantiate(projectileSpawner.ProjectilePrefab);
                    
                    ecb.SetComponent(
                        newProjectileEntity,
                        LocalTransform.FromPosition(projectileSpawner.ProjectileSpawnPosition));
                    
                    ecb.AddComponent(
                        newProjectileEntity,
                        new ProjectileMovementComponent
                        {
                            Direction = projectileSpawner.ProjectileDirection,
                            Speed = projectileSpawner.ProjectileSpeed,
                            Lifetime = 0f
                        });
                    
                    ecb.AddComponent(newProjectileEntity,
                        new PlayerDamagerComponent
                        {
                            DamageOnHit = 10f   //TODO
                        });
                }
            }
        }
    }
}