using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AvatarController : NetworkBehaviour
{
    private Transform headTransform;
    private Vector3 spawnPosition;

    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();

        //Added to allow separate player spawn locations.
        //Moves the origin of the quest tracking to be at the spawn position
        if (!IsOwner){
            return;
        }
        spawnPosition = transform.position;
        GameObject xrRig = GameObject.Find("XR Origin (XR Rig)");
        xrRig.transform.position = spawnPosition;
        
        headTransform = Camera.main.transform;
    }
    void LateUpdate(){
        if (!IsOwner){
            return;
        }

        // When you swap scenes the camera might change. Update the transform here.
        if (headTransform == null){
            GameObject xrRig = GameObject.Find("XR Origin (XR Rig)");
            xrRig.transform.position = spawnPosition;
            headTransform = Camera.main.transform;
        }
        this.transform.position = headTransform.position;
        this.transform.rotation = headTransform.rotation;

    }
    

}
