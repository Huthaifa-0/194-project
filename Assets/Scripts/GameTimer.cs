using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using System;


public class GameTimer : NetworkBehaviour
{
   [SerializeField]
   private TMP_Text uiLabel;
   private NetworkVariable<float> gameTimer = new NetworkVariable<float>(100f);


   public override void OnNetworkSpawn(){
       base.OnNetworkSpawn();
       gameTimer.OnValueChanged += gameTimer_OnValueChanged;
   }


   private void gameTimer_OnValueChanged(float prevVal, float newValue){
       uiLabel.text = "Timer: "+newValue+" sec Remaining";
   }


   void Update(){
       if (!IsServer) return;


       if (gameTimer.Value > 0){
           gameTimer.Value -= Time.deltaTime;
           if (gameTimer.Value < 0){
               TimerExpiredClientRpc("Game Over");
           }
       }
      
   }


   [Rpc(SendTo.ClientsAndHost)]
   void TimerExpiredClientRpc(String message){
gameTimer.OnValueChanged -= gameTimer_OnValueChanged;
       uiLabel.text = message;
   }
      
  
}
