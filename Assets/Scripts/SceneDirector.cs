using Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    private void Awake()
    {
        /*var playerWeaponParticleEffectSystem =
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<GameLoopSystem>();
            
        playerWeaponParticleEffectSystem.Init(this);*/
    }
    
    void Start()
    {
        /*Debug.Log($"loadedSceneCount: {SceneManager.loadedSceneCount}");
        for (int i = 0; i < SceneManager.loadedSceneCount; i++)
        {
            Debug.Log($"scene {i}: {SceneManager.GetSceneAt(i).name}");
        }*/
    }
    
    public void OnPlayerDied()
    {
        //Debug.Log($"Player died!");

        //var gameSubscene = SceneManager.GetSceneByName("TurretDodge");
        //SceneManager.UnloadSceneAsync(gameSubscene);
        //SceneManager.LoadScene("TurretDodge");
    }
}
