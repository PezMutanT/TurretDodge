using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PlayerMovementSystem))]
    public partial struct PlayerPositionUpdatingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerSingleton = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponentRO<LocalTransform>(playerSingleton);
            var currentPlayerPosition = playerTransform.ValueRO.Position;
            foreach (var playerPositionComponent in SystemAPI.Query<RefRW<PlayerPositionComponent>>())
            {
                playerPositionComponent.ValueRW.Value = currentPlayerPosition;
            }
        }
    }
}