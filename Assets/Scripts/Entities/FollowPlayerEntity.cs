using Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Entities
{
    public class FollowPlayerEntity : MonoBehaviour
    {
        public Vector3 _offset;

        private LocalTransform _playerEntityTransform;
        private EntityManager _entityManager;
        private EntityQuery _query;
        private Entity _playerEntity;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _query = _entityManager.CreateEntityQuery(new ComponentType[]
            {
                typeof(PlayerTag)
            });
        }

        private void LateUpdate()
        {
            _query.TryGetSingletonEntity<PlayerTag>(out _playerEntity);
            if (_playerEntity == Entity.Null)
            {
                Debug.Log($"LateUpdate with entity null");
                return;
            }

            _playerEntityTransform = _entityManager.GetComponentData<LocalTransform>(_playerEntity);

            Vector3 currentTargetPosition = _playerEntityTransform.Position;
            transform.position = currentTargetPosition + _offset;
        }
    }
}