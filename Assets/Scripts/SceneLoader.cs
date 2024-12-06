using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : NetworkBehaviour
{
   public const string mainSceneName = "MainScene";
  
   public void LoadMainScene(){
       NetworkManager.Singleton.SceneManager.LoadScene(mainSceneName, LoadSceneMode.Single);
   }
}