using Unity.Entities;

namespace Components
{
    public struct EnemyDamagerComponent : IComponentData
    {
        public float DamagePerSecond;
    }
}