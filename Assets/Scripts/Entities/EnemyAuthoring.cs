using Components;
using Unity.Entities;
using UnityEngine;

namespace Entities
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float Speed;
        
        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new EnemyMovementComponent
                {
                    Speed = authoring.Speed
                });
            }
        }
    }
}