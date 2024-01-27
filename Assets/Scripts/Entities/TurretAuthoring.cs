using Components;
using Unity.Entities;
using UnityEngine;

namespace Entities
{
    public class TurretAuthoring : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public float FrequenceInSeconds;
        public Vector3 Direction;
        public float Speed;

        private class TurretAuthoringBaker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ProjectileSpawningComponent
                {
                    ProjectilePrefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
                    FrequenceInSeconds = authoring.FrequenceInSeconds,
                    Direction = authoring.Direction,
                    Speed = authoring.Speed
                });

                AddComponent(entity, new ProjectileSpawningTimerComponent
                {
                    Value = authoring.FrequenceInSeconds
                });
                
                AddComponent(entity, new ProjectileInitializerComponent
                {
                    
                });
            }
        }
    }
}