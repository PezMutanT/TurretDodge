using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct EnemySpawningAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRW<EnemySpawningComponent> _enemySpawningComponent;
        private readonly RefRO<PlayerPositionComponent> _playerPositionComponent;
        
        public float SpawnTimer
        {
            get => _enemySpawningComponent.ValueRO.SpawnTimer;
            set => _enemySpawningComponent.ValueRW.SpawnTimer = value;
        }

        public float SpawnFrequence => _enemySpawningComponent.ValueRO.SpawnFrequence;
        public Entity EnemyPrefab => _enemySpawningComponent.ValueRO.EnemyPrefab;
        public float3 PlayerPosition => _playerPositionComponent.ValueRO.Value;
        public float2 SpawnRadius => _enemySpawningComponent.ValueRO.SpawnRadius;
        public int SpawnAmount => _enemySpawningComponent.ValueRO.SpawnAmount;
    }
}