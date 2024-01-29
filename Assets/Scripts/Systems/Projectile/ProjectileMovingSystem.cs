using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    //[UpdateBefore(typeof(PlayerMovementSystem))]
    public partial struct ProjectileMovingSystem : ISystem
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
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, projectileMovement) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<ProjectileMovementComponent>>())
            {
                transform.ValueRW.Position +=
                    projectileMovement.ValueRO.Direction * projectileMovement.ValueRO.Speed * deltaTime;
            }
        }
    }
}