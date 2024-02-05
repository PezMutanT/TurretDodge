using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial class PlayerWeaponParticleEffectSystem : SystemBase
    {
        private ParticleSystem _particleSystem;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            Enabled = false;
        }

        public void Init(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
            
            Enabled = true;
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        protected override void OnUpdate()
        {
            foreach (var (particleSystem, transform) in
                     SystemAPI.Query<ParticleSystemComponent, RefRW<LocalTransform>>())
            {
                _particleSystem.transform.rotation = Quaternion.LookRotation(particleSystem.Direction, math.up());
                _particleSystem.transform.position = transform.ValueRO.Position;
                //transform.ValueRW = LocalTransform.FromPosition(_particleSystem.transform.position);
                _particleSystem.Play();
            }
        }
    }
}