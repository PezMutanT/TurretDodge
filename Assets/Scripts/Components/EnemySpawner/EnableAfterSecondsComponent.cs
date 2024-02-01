using Unity.Entities;

namespace Components
{
    public struct EnableAfterSecondsComponent : IComponentData, IEnableableComponent
    {
        public float Value;
    }
}