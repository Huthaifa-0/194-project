using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AvatarController : NetworkBehaviour
{
   private Transform headTransform;

   public override void OnNetworkSpawn(){
       base.OnNetworkSpawn();
       headTransform = Camera.main.transform;
   }
   void LateUpdate(){ 
       if (!IsOwner) return;

	// When you swap scenes the camera might change. Update the transform here.
       if (headTransform == null){
           headTransform = Camera.main.transform;
       }

       this.transform.position = headTransform.position;
       this.transform.rotation = headTransform.rotation;

   }

}
