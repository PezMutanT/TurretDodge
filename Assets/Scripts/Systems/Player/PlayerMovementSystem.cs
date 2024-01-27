using Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMovementSystem : ISystem
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
            foreach (var player in SystemAPI.Query<PlayerMovementAspect>())
            {
                player.MoveFromInput(deltaTime);
            }
        }
    }
}