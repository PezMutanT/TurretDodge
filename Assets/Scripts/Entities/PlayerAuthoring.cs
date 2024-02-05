using Unity.Entities;
using UnityEngine;
using Components;

namespace Entities
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float Speed;
        public int MaxHealth;
        public float DamagePerSecond;
        public float FireRate;
        public GameObject PlayerProjectilePrefab;

        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new PlayerTag());
                AddComponent(entity, new PlayerInputComponent());
                AddComponent(entity, new PlayerSpeed
                {
                    Value = authoring.Speed
                });
                
                AddComponent(entity, new PlayerHealthComponent
                {
                    Value = authoring.MaxHealth
                });
                
                AddComponent(entity, new CameraTargetTag());

                AddComponent(entity, new PlayerFiringComponent
                {
                    PlayerProjectilePrefab = GetEntity(authoring.PlayerProjectilePrefab, TransformUsageFlags.Dynamic),
                    FireRate = authoring.FireRate,
                    DamagePerSecond = authoring.DamagePerSecond,
                    Timer = authoring.FireRate
                });
            }
        }
    }
}