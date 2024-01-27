using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ProjectileInitializerComponent : IComponentData, IEnableableComponent
    {
        public float3 Direction;
    }
}