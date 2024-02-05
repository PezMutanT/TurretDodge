using Systems;
using Unity.Entities;
using UnityEngine;

namespace Entities
{
    public class ParticleSystemInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            var playerWeaponParticleEffectSystem =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerWeaponParticleEffectSystem>();
            
            playerWeaponParticleEffectSystem.Init(_particleSystem);
        }

    }
}