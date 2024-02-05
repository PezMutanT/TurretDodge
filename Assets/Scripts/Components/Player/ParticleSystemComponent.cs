using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ParticleSystemComponent : IComponentData, IEnableableComponent
    {
        public float3 Direction;
    }
}