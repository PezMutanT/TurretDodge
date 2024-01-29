using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    //[UpdateAfter(typeof(PlayerCollisionDetectionSystem))]
    public partial struct PlayerDeathCheckingSystem : ISystem
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
            foreach (var (playerHealth, entity) in SystemAPI.Query<PlayerHealthComponent>().WithEntityAccess())
            {
                if (playerHealth.Value <= 0)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}