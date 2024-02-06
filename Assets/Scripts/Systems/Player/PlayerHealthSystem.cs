using Components;
using UserInterface;
using Unity.Entities;

namespace Systems
{
    public partial class PlayerHealthSystem : SystemBase
    {
        private PlayerHealthBarUI _playerHealthBarUI;

        protected override void OnCreate()
        {
            Enabled = false;
            
            RequireForUpdate<PlayerHealthComponent>();
        }

        public void Init(PlayerHealthBarUI playerHealthBarUI)
        {
            _playerHealthBarUI = playerHealthBarUI;

            Enabled = true;
        }

        protected override void OnUpdate()
        {
            if (!_playerHealthBarUI.IsInitialized())
            {
                _playerHealthBarUI.Init();
            }
            
            foreach (var playerHealth in SystemAPI.Query<RefRO<PlayerHealthComponent>>())
            {
                if (!_playerHealthBarUI.HasPlayerHealthChanged(playerHealth.ValueRO.Value))
                {
                    continue;
                }
                
                _playerHealthBarUI.RefreshHealthValue(playerHealth.ValueRO.Value);
            }
        }
    }
}