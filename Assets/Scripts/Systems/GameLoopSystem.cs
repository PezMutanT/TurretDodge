using Components;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

namespace Systems
{
    public partial struct GameLoopSystem : ISystem
    {

        //private SceneDirector _sceneDirector;
        
        /*protected override void OnCreate()
        {
            Enabled = false;
            RequireForUpdate<DeadPlayer>();
        }

        public void Init(SceneDirector sceneDirector)
        {
            Debug.Log($"GameLoopSystem:Init");
            
            _sceneDirector = sceneDirector;
            
            Enabled = true;
        }*/

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DeadPlayer>();
        }

        public void OnUpdate(ref SystemState state)
        {
            Debug.Log($"GameLoopSystem:OnUpdate");
            foreach (var (deadPlayer, sceneLoader) in SystemAPI.Query<DeadPlayer, SceneLoaderComponent>())
            {
                //_sceneDirector.OnPlayerDied();

                var sceneGUID = sceneLoader.SceneGUID;
                Debug.Log($"SceneGUID: {sceneGUID}");
                var unloadParameters = SceneSystem.UnloadParameters.DestroyMetaEntities;
                SceneSystem.UnloadScene(state.WorldUnmanaged, sceneGUID, unloadParameters);

                state.Enabled = false;
            }
        }
    }
}