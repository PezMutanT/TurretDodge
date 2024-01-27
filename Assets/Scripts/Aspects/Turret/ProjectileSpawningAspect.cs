using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct ProjectileSpawningAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRW<ProjectileInitializerComponent> _projectileInitializer;
        private readonly RefRW<ProjectileSpawningTimerComponent> _projectileSpawningTimer;
        private readonly RefRO<ProjectileSpawningComponent> _projectileSpawn;

        public Entity ProjectilePrefab => _projectileSpawn.ValueRO.ProjectilePrefab;
        public float SpawnProjectileTimer
        {
            get => _projectileSpawningTimer.ValueRO.Value;
            set => _projectileSpawningTimer.ValueRW.Value = value;
        }

        public bool IsTimeToSpawnProjectile => SpawnProjectileTimer <= 0f;
        public float SpawnProjectileFrequency => _projectileSpawn.ValueRO.FrequenceInSeconds;
        public float3 ProjectileSpawnPosition => _localTransform.ValueRO.Position;
        public float3 ProjectileDirection => _projectileSpawn.ValueRO.Direction;
        public float ProjectileSpeed => _projectileSpawn.ValueRO.Speed;
    }
}