using Components;
using Unity.Entities;
using UnityEngine;

namespace Entities
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public float SpawnInitialTimeAfterGameStart;
        public float SpawnRadius;
        public float SpawnFrequence;
        public int SpawnAmount;
        
        private class EnemySpawnerAuthoringBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new EnemySpawningComponent
                {
                    EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                    SpawnRadius = authoring.SpawnRadius,
                    SpawnFrequence = authoring.SpawnFrequence,
                    SpawnAmount = authoring.SpawnAmount,
                    SpawnTimer = authoring.SpawnFrequence
                });

                AddComponent(entity, new PlayerPositionComponent());
                
                AddComponent(entity, new EnableAfterSecondsComponent
                {
                    Value = authoring.SpawnInitialTimeAfterGameStart
                });
                SetComponentEnabled<EnableAfterSecondsComponent>(true);
            }
        }

        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Vector3.zero, SpawnRadius);
        }*/
    }
}