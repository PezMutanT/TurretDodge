using Components;
using Unity.Entities;
using UnityEngine;

namespace Entities
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float Speed;
        public float DamageOnHit;
        public float DamageRadius;
        public float RefocusPlayerFrequence;
        public float MaxHealth;

        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new EnemyMovementComponent
                {
                    Speed = authoring.Speed,
                    RefocusPlayerFrequence = authoring.RefocusPlayerFrequence
                });

                AddComponent(entity, new PlayerPositionComponent());

                AddComponent(entity, new RefocusPlayerTimerComponent
                {
                    Value = authoring.RefocusPlayerFrequence
                });
                
                AddComponent(entity, new PlayerDamagerComponent
                {
                    DamageOnHit = authoring.DamageOnHit,
                    DamageRadiusSquared = authoring.DamageRadius * authoring.DamageRadius
                });

                AddComponent(entity, new EnemyHealthComponent
                {
                    Value = authoring.MaxHealth
                });
            }
        }
    }
}