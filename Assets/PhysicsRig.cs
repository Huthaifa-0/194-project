using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public Transform playerHead;
    public Transform leftControler;
    public Transform rightControler;

    public ConfigurableJoint headJoint;
    public ConfigurableJoint lefttHandJoint;
    public ConfigurableJoint rightHandJoint;

    public CapsuleCollider bodyCollider;

    public float bodyHightMin = 0.5f;
    public float bodyHightMax = 2f;

    // Update is called once per frame
    void FixedUpdate()
    {
       bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y,bodyHightMin,bodyHightMax);
       bodyCollider.center = new Vector3 (playerHead.localPosition.x,bodyCollider.height/2,
            playerHead.localPosition.z);
            lefttHandJoint.targetPosition = leftControler.localPosition;
            lefttHandJoint.targetRotation = leftControler.localRotation;

            rightHandJoint.targetPosition = rightControler.localPosition;
            rightHandJoint.targetRotation = rightControler.localRotation;
            
            headJoint.targetPosition = playerHead.localPosition;
        
    }
}
