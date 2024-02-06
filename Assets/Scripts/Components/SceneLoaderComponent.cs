using Unity.Entities;

namespace Components
{
    public struct SceneLoaderComponent : IComponentData
    {
        public Hash128 SceneGUID;
    }
}