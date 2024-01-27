using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    public partial struct ProjectileSelfKillingSystem : ISystem
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
            float deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (projectile, entity) in
                     SystemAPI.Query<RefRW<ProjectileMovementComponent>>().WithEntityAccess())
            {
                projectile.ValueRW.Lifetime += deltaTime;
                if (projectile.ValueRO.Lifetime >= 10f)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}