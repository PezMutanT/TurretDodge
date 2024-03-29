﻿using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace Aspects
{
    public readonly partial struct PlayerMovementAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<PhysicsVelocity> _physicsVelocity;
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<PlayerInputComponent> _playerInput;
        private readonly RefRO<PlayerSpeed> _playerSpeed;
        private readonly RefRO<PhysicsMass> _physicsMass;

        public void MoveFromInput(float deltaTime)
        {
            float3 direction = new float3(
                _playerInput.ValueRO.PlayerDirection.x,
                0f,
                _playerInput.ValueRO.PlayerDirection.y);
            
            // _physicsVelocity.ValueRW.ApplyLinearImpulse(_physicsMass.ValueRO, newVelocity * _playerSpeed.ValueRO.Value);
            _physicsVelocity.ValueRW.Linear = direction * _playerSpeed.ValueRO.Value * deltaTime;
            /*_localTransform.ValueRW.Position =
                _localTransform.ValueRO.Position + newVelocity * _playerSpeed.ValueRO.Value * deltaTime;*/

            if (direction.Equals(float3.zero))
            {
                return;
            }
            
            _localTransform.ValueRW.Rotation = quaternion.LookRotation(direction, math.up());
        }
    }
}