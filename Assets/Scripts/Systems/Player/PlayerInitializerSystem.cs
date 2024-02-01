using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerInitializerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            
            foreach (var mass in SystemAPI.Query<RefRW<PhysicsMass>>().WithAll<PlayerTag>())
            {
                mass.ValueRW.InverseMass = 0.1f;
            }
        }
    }
}