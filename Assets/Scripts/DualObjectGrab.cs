using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DualObjectGrab : MonoBehaviour
{
    [Header("References")]
    public Transform physicalController; // Reference to the physical controller
    public Transform virtualHand;        // Reference to the virtual hand
    public InputActionAsset actions;

    [Header("Grab Settings")]
    public float grabRadius = 0.1f;      // Radius to check for grabbable objects
    public LayerMask grabbableLayer;     // Layer for grabbable objects

    private InputAction grabAction;
    private GameObject objectInHand;
    private Transform activeGrabPoint;    // Which transform is currently holding the object

    // Keep track of objects within grab range of both positions
    private List<GameObject> objectsNearController = new List<GameObject>();
    private List<GameObject> objectsNearVirtualHand = new List<GameObject>();

    void Awake()
    {
        grabAction = actions.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        grabAction.performed += GrabObject;
        grabAction.canceled += ReleaseObject;
    }

    void OnEnable()
    {
        grabAction.Enable();
    }

    void OnDisable()
    {
        grabAction.Disable();
    }

    void Update()
    {
        // Update grabbable objects lists
        if (objectInHand == null)
        {
            UpdateGrabbableObjects();
        }
    }

    void UpdateGrabbableObjects()
    {
        // Clear previous lists
        objectsNearController.Clear();
        objectsNearVirtualHand.Clear();

        // Check for objects near physical controller
        Collider[] nearControllerColliders = Physics.OverlapSphere(physicalController.position, grabRadius, grabbableLayer);
        foreach (Collider col in nearControllerColliders)
        {
            if (col.attachedRigidbody != null)
            {
                objectsNearController.Add(col.gameObject);
            }
        }

        // Check for objects near virtual hand
        Collider[] nearVirtualHandColliders = Physics.OverlapSphere(virtualHand.position, grabRadius, grabbableLayer);
        foreach (Collider col in nearVirtualHandColliders)
        {
            if (col.attachedRigidbody != null)
            {
                objectsNearVirtualHand.Add(col.gameObject);
            }
        }
    }

    void GrabObject(InputAction.CallbackContext context)
    {
        if (objectInHand != null) return;

        GameObject objectToGrab = null;
        Transform grabPoint = null;

        // First priority: Check physical controller
        if (objectsNearController.Count > 0)
        {
            objectToGrab = GetClosestObject(objectsNearController, physicalController.position);
            grabPoint = physicalController;
        }
        // Second priority: Check virtual hand
        else if (objectsNearVirtualHand.Count > 0)
        {
            objectToGrab = GetClosestObject(objectsNearVirtualHand, virtualHand.position);
            grabPoint = virtualHand;
        }

        if (objectToGrab != null)
        {
            // Store which transform is holding the object
            activeGrabPoint = grabPoint;

            // Grab the object
            objectInHand = objectToGrab;
            Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            
            // Parent to the appropriate transform
            objectInHand.transform.SetParent(activeGrabPoint);

            // Optional: Adjust position
            Vector3 originalScale = objectInHand.transform.localScale;
            objectInHand.transform.localPosition = Vector3.zero;
            objectInHand.transform.localRotation = Quaternion.identity;
            objectInHand.transform.localScale = originalScale; // Preserve original scale
        }
    }

    void ReleaseObject(InputAction.CallbackContext context)
    {
        if (objectInHand != null)
        {
            Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            
            // Optional: Add velocity on release
            if (rb != null)
            {
                Vector3 throwVelocity = CalculateThrowVelocity();
                rb.velocity = throwVelocity;
            }

            objectInHand.transform.SetParent(null);
            objectInHand = null;
            activeGrabPoint = null;
        }
    }

    GameObject GetClosestObject(List<GameObject> objects, Vector3 position)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }

    Vector3 CalculateThrowVelocity()
    {
        // You can implement throwing velocity calculation here
        // For now, returning a simple velocity
        return activeGrabPoint.forward * 2f;
    }

    // Optional: Visualization for debugging
    void OnDrawGizmos()
    {
        if (physicalController != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(physicalController.position, grabRadius);
        }
        if (virtualHand != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(virtualHand.position, grabRadius);
        }
    }
}