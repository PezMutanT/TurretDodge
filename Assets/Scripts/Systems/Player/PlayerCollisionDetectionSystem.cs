using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup), OrderLast = true)]
    public partial struct PlayerCollisionDetectionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = new PlayerCollisionDetectionJob
            {

            }.Schedule(simulationSingleton, state.Dependency);
        }
    }
    
    public struct PlayerCollisionDetectionJob : ITriggerEventsJob
    {
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log($"Trigger event: {triggerEvent.EntityA} and {triggerEvent.EntityB}");
        }
    }
}