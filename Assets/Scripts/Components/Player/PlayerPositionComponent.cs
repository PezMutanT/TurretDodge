using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct PlayerPositionComponent : IComponentData
    {
        public float3 Value;
    }
}