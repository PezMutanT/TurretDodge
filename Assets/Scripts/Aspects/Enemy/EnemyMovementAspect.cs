using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct EnemyMovementAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<EnemyMovementComponent> _enemyMovementComponent;
        private readonly RefRO<PlayerPositionComponent> _playerPositionComponent;
        private readonly RefRO<PlayerDamagerComponent> _playerDamagerComponent;

        public float3 Position
        {
            get => _localTransform.ValueRO.Position;
            set => _localTransform.ValueRW.Position = value;
        }

        public void MoveForward(float deltaTime)
        {
            var newPosition =
                _localTransform.ValueRW.Position +
                _localTransform.ValueRO.Forward() * _enemyMovementComponent.ValueRO.Speed * deltaTime;

            if (math.distancesq(newPosition, _playerPositionComponent.ValueRO.Value) <
                 _playerDamagerComponent.ValueRO.DamageRadiusSquared)
            {
                return;
            }
            
            _localTransform.ValueRW.Position = newPosition;
        }
    }
}