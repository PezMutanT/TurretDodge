using Unity.Entities;
using UnityEngine;
using Components;

namespace Entities
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float Speed;
        
        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new PlayerTag());
                AddComponent(entity, new PlayerInputComponent());
                AddComponent(entity, new PlayerSpeed
                {
                    Value = authoring.Speed
                });
                
                AddComponent(entity, new PlayerHealthComponent
                {
                    Value = 100f
                });
                
                /*AddComponent(entity, new CameraTargetTag());*/
            }
        }
    }
}