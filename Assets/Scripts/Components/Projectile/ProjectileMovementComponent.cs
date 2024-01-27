using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ProjectileMovementComponent : IComponentData
    {
        public float3 Direction;
        public float Speed;
    }
}