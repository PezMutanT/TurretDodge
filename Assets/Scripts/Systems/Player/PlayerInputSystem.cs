/*using Unity.Entities;
using UnityEngine;
using Components;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputSystem : SystemBase
    {
        private InputActions _inputActions;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerInputComponent>();

            _inputActions = new InputActions();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            _inputActions.Enable();
        }

        protected override void OnStopRunning()
        {
            base.OnStopRunning();

            _inputActions.Disable();
        }

        protected override void OnUpdate()
        {
            var currentMoveInput = _inputActions.InputActionMap.PlayerDirection.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new PlayerInputComponent
            {
                PlayerDirection = currentMoveInput
            });
        }
    }
}*/