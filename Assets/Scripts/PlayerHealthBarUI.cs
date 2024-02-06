using Components;
using Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image _progressBarMask;
        private PlayerHealthComponent _playerHealth;
        private float _maxHealth;
        private float _currentHealth;
        private bool _isInitialized;

        private void Awake()
        {
            _isInitialized = false;
            
            var playerHealthSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerHealthSystem>();
            playerHealthSystem.Init(this);
        }

        public bool IsInitialized() => _isInitialized;

        public bool HasPlayerHealthChanged(float newHealth) => newHealth != _currentHealth;

        public void Init()
        {
            _isInitialized = true;
            
            EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery entityQuery = _entityManager.CreateEntityQuery(new ComponentType[]
            {
                typeof(PlayerHealthComponent)
            });
            
            _playerHealth = entityQuery.GetSingleton<PlayerHealthComponent>();
            _maxHealth = _playerHealth.Value;
        }

        public void RefreshHealthValue(float newHealth)
        {
            if (_currentHealth == newHealth)
            {
                return;
            }

            _currentHealth = newHealth;

            _progressBarMask.fillAmount = newHealth / _maxHealth;
        }
    }
}