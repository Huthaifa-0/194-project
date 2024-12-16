using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyUI : MonoBehaviour
{
   [SerializeField]
   private Button PlayBeach;

   [SerializeField]
   private Button PlayWater;


   void Awake(){
       PlayWater.onClick.AddListener( () => {
           MultiPlayer.Instance.CreateLobby("DefaultLobby", false);
       });


       PlayBeach.onClick.AddListener( ()=> {
           MultiPlayer.Instance.QuickJoin();
       });
   }
}
