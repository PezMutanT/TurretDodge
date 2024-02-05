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
        public float3 FirePosition => _localTransform.ValueRO.Position + _localTransform.ValueRO.Forward() * 1.5f;      //TODO - make it a GameObject's transform inside prefab

        public float DamagePerSecond => _playerFiringComponent.ValueRO.DamagePerSecond;
        public float3 PlayerDirection => _localTransform.ValueRO.Forward();
    }
}