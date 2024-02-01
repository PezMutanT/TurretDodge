using Aspects;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemySpawningSystem : ISystem
    {
        private Random rng;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            rng = new Random(123);
            state.RequireForUpdate<EnemySpawningComponent>();
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

            foreach (var (enemySpawnerEnableTimer, enemySpawnerEnabled) in
                     SystemAPI.Query<RefRW<EnableAfterSecondsComponent>, EnabledRefRW<EnableAfterSecondsComponent>>())
            {
                enemySpawnerEnableTimer.ValueRW.Value -= deltaTime;
                if (enemySpawnerEnableTimer.ValueRO.Value <= 0f)
                {
                    enemySpawnerEnabled.ValueRW = false;
                }
            }

            foreach (var enemySpawner in
                     SystemAPI.Query<EnemySpawningAspect>().WithDisabled<EnableAfterSecondsComponent>())
            {
                enemySpawner.SpawnTimer -= deltaTime;
                if (enemySpawner.SpawnTimer <= 0f)
                {
                    enemySpawner.SpawnTimer = enemySpawner.SpawnFrequence;

                    var newEntityArray = new NativeArray<Entity>(enemySpawner.SpawnAmount, Allocator.Temp);
                    ecb.Instantiate(enemySpawner.EnemyPrefab, newEntityArray);
                    foreach (var newEntity in newEntityArray)
                    {
                        var randomPositionAroundPlayer = rng.NextFloat2Direction() * enemySpawner.SpawnRadius;
                        var playerPosition = enemySpawner.PlayerPosition;
                        var newEnemyPosition = new float3(
                            playerPosition.x + randomPositionAroundPlayer.x,
                            1f,
                            playerPosition.z + randomPositionAroundPlayer.y);

                        var enemyRotation =
                            quaternion.LookRotation((playerPosition - newEnemyPosition), math.up());
                        
                        ecb.SetComponent(
                            newEntity,
                            LocalTransform.FromPositionRotation(newEnemyPosition, enemyRotation));
                    }

                    newEntityArray.Dispose();
                }
            }
        }
    }
}