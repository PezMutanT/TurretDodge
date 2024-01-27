using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct PlayerInputComponent : IComponentData
    {
        public float2 PlayerDirection;
    }
}