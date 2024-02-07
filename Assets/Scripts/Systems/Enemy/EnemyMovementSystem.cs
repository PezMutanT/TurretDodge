using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PlayerMovementSystem))]
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
            var deltaTime = SystemAPI.Time.fixedDeltaTime;
            foreach (var enemy in
                     SystemAPI.Query<EnemyMovementAspect>())
            {
                enemy.MoveForward(deltaTime);
            }
        }
    }
}