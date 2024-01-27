using Unity.Entities;

namespace Components
{
    public struct PlayerDamagerComponent : IComponentData
    {
        public float DamageOnHit;
    }
}