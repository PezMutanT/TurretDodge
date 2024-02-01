using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial struct EnemyMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyMovementComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, enemy) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemyMovementComponent>>())
            {
                transform.ValueRW.Position += transform.ValueRO.Forward() * enemy.ValueRO.Speed * deltaTime;
            }
        }
    }
}