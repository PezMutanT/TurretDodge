using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;

namespace Entities
{
    public class FollowEntity : MonoBehaviour
    {
        public Vector3 _offset;

        private LocalTransform _cameraTargetEntityTransform;
        private EntityManager _entityManager;
        private EntityQuery _query;
        private Entity _cameraTargetEntity;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _query = _entityManager.CreateEntityQuery(new ComponentType[]
            {
                typeof(CameraTargetTag)
            });
        }

        private void LateUpdate()
        {
            _query.TryGetSingletonEntity<CameraTargetTag>(out _cameraTargetEntity);
            if (_cameraTargetEntity == Entity.Null)
            {
                Debug.Log($"LateUpdate with entity null");
                return;
            }

            _cameraTargetEntityTransform = _entityManager.GetComponentData<LocalTransform>(_cameraTargetEntity);

            Vector3 currentTargetPosition = _cameraTargetEntityTransform.Position;
            transform.position = currentTargetPosition + _offset;
        }
    }
}