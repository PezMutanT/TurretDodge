using Unity.Entities;

namespace Components
{
    public struct EnemySpawningComponent : IComponentData
    {
        public Entity EnemyPrefab;
        public float SpawnRadius;
        public float SpawnFrequence;
        public int SpawnAmount;
        public float SpawnTimer;
    }
}