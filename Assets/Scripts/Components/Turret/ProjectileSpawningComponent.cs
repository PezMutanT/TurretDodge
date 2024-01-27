using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ProjectileSpawningComponent : IComponentData
    {
        public Entity ProjectilePrefab;
        public float FrequenceInSeconds;
        public float3 Direction;
        public float Speed;
    }
}