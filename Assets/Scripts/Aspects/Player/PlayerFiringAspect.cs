using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerFiringAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRW<PlayerFiringComponent> _playerFiringComponent;

        public Entity ProjectilePrefab => _playerFiringComponent.ValueRO.PlayerProjectilePrefab;

        public float FireTimer
        {
            get => _playerFiringComponent.ValueRO.Timer;
            set => _playerFiringComponent.ValueRW.Timer = value;
        }

        public float FireRate => _playerFiringComponent.ValueRO.FireRate;
        public float3 FirePosition => new float3(
            _localTransform.ValueRO.Position.x,
            _localTransform.ValueRO.Position.y,
            _localTransform.ValueRO.Position.z + 1f);      //TODO

        public float DamagePerSecond => _playerFiringComponent.ValueRO.DamagePerSecond;
    }
}