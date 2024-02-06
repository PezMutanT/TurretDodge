using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [UpdateAfter(typeof(EnemyMovementSystem))]
    public partial struct EnemyDamageByContactSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<PlayerHealthComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerHealthLookup = SystemAPI.GetComponentLookup<PlayerHealthComponent>();
            
            foreach (var enemy in SystemAPI.Query<EnemyDamageAspect>())
            {
                if (enemy.HasToDamagePlayer(deltaTime))
                {
                    playerHealthLookup.GetRefRW(playerEntity).ValueRW.Value -= enemy.DamagePerSecond * deltaTime;
                }
            }
        }
    }
}