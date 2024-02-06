using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct EnemyMovementAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRW<PhysicsVelocity> _physicsVelocity;
        private readonly RefRO<EnemyMovementComponent> _enemyMovementComponent;
        private readonly RefRO<PlayerPositionComponent> _playerPositionComponent;
        private readonly RefRW<RefocusPlayerTimerComponent> _refocusPlayerTimerComponent;
        private readonly RefRO<PlayerDamagerComponent> _playerDamagerComponent;

        public float3 Position
        {
            get => _localTransform.ValueRO.Position;
            set => _localTransform.ValueRW.Position = value;
        }

        public void MoveForward(float deltaTime)
        {
            var newRotation = _localTransform.ValueRO.Rotation;
            _refocusPlayerTimerComponent.ValueRW.Value -= deltaTime;
            if (_refocusPlayerTimerComponent.ValueRO.Value < 0f)
            {
                _refocusPlayerTimerComponent.ValueRW.Value = _enemyMovementComponent.ValueRO.RefocusPlayerFrequence;
                
                newRotation = quaternion.LookRotation((_playerPositionComponent.ValueRO.Value - Position), math.up());
                _localTransform.ValueRW.Rotation = newRotation;
            }

            var newPosition =
                _localTransform.ValueRW.Position +
                _localTransform.ValueRO.Forward() * _enemyMovementComponent.ValueRO.Speed * deltaTime;

            if (math.distancesq(newPosition, _playerPositionComponent.ValueRO.Value) <
                 _playerDamagerComponent.ValueRO.DamageRadiusSquared)
            {
                _physicsVelocity.ValueRW.Linear = float3.zero;
                return;
            }
            
            //_localTransform.ValueRW = LocalTransform.FromPositionRotation(newPosition, newRotation);

            var newVelocity = _localTransform.ValueRO.Forward();
            _physicsVelocity.ValueRW.Linear = newVelocity * _enemyMovementComponent.ValueRO.Speed * deltaTime;
        }
    }
}