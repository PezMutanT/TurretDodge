using Unity.Entities;

namespace Components
{
    public struct PlayerFiringComponent : IComponentData
    {
        public Entity PlayerProjectilePrefab;
        public float FireRate;
        public float DamagePerSecond;
        public float Timer;
    }
}