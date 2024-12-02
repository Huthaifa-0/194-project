using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrab : MonoBehaviour
{
    public Transform controller;
    public GameObject collidingObject;
    public GameObject objectInHand;
    public InputActionAsset actions;
    private InputAction grabAction;

    void Awake()
    {
        // Updated to use XRI Default Input Actions
        if (controller.CompareTag("Right")){
        grabAction = actions.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
        grabAction.performed += GrabObject;
        grabAction.canceled += ReleaseObject;
        Debug.Log("Right Controler Grab action setup complete");
        }
        else
        {
            grabAction = actions.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        grabAction.performed += GrabObject;
        grabAction.canceled += ReleaseObject;
        Debug.Log("Left Controler Grab action setup complete");
        }
    }

    public void OnEnable()
    {
        grabAction.Enable();
        Debug.Log("Grab action enabled");
    }

    public void OnDisable()
    {
        grabAction.Disable();
        Debug.Log("Grab action disabled");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered with: {other.gameObject.name}");
        if(other.gameObject.GetComponent<Rigidbody>()) {
            collidingObject = other.gameObject;
            Debug.Log($"Colliding object set to: {collidingObject.name}");
        } 
        else {
            Debug.Log("Object has no Rigidbody component");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        collidingObject = null;
    }

    public void GrabObject(InputAction.CallbackContext context)
    {
        Debug.Log("Grab action performed");
        if (collidingObject != null) {
            Debug.Log($"Attempting to grab: {collidingObject.name}");
            objectInHand = collidingObject;
            
            objectInHand.transform.SetParent(this.transform);
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;

            Debug.Log($"Object in hand: {objectInHand.name}");

            Debug.Log("Grab successful");
        } else {
            Debug.Log("No object to grab");
        }
    }

    public void ReleaseObject(InputAction.CallbackContext context)
    {
        Debug.Log("Release action performed");
        if (objectInHand != null) {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null);
            objectInHand = null;
            Debug.Log("Object released");
        }
    }
}