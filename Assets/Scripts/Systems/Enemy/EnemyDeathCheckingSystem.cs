using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    [UpdateBefore(typeof(PlayerDeathCheckingSystem))]
    public partial struct EnemyDeathCheckingSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
            foreach (var (enemyHealth, entity) in SystemAPI.Query<EnemyHealthComponent>().WithEntityAccess())
            {
                if (enemyHealth.Value <= 0)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}