using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyInitializerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsMass>();
            state.RequireForUpdate<EnemyInitializationComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, enemyInitialization) in
                     SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<EnemyInitializationComponent>>())
            {
                enemyInitialization.ValueRW = false;
                mass.ValueRW.InverseMass = 0.1f;
            }
        }
    }
}