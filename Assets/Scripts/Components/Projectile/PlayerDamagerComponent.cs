using Unity.Entities;

namespace Components
{
    public struct PlayerDamagerComponent : IComponentData
    {
        public float DamagePerSecond;
        public float DamageRadiusSquared;
    }
}