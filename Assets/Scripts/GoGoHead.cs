using UnityEngine;
using UnityEngine.XR;

public class GoGoHead : MonoBehaviour
{
    [Header("GoGo Technique Parameters")]
    [Tooltip("Distance threshold where non-linear scaling begins")]
    public float threshold = 0.5f; // Distance from head where scaling starts
    
    [Tooltip("Coefficient for non-linear scaling")]
    public float k = 2.0f;
    
    [Tooltip("Maximum allowed distance for the virtual hand")]
    public float maxDistance = 20.0f;
    
    [Header("References")]
    [Tooltip("Reference to the VR controller transform")]
    public Transform controllerTransform;
    
    [Tooltip("Reference to the virtual hand transform")]
    public Transform virtualHandTransform;
    
    private Transform cameraTransform;


    private void Start()
    {
        if (virtualHandTransform == null || controllerTransform == null)
        {
            Debug.LogError("GoGoTechnique: Required transforms not assigned!");
            enabled = false;
            return;
        }

        cameraTransform = Camera.main.transform;
    }



    private void Update()
    {
        UpdateHandPosition();
    }

    private void UpdateHandPosition()
    {
        // Get the controller position relative to the head
        Vector3 handToHead = controllerTransform.position - cameraTransform.position;
        float distanceFromHead = handToHead.magnitude;

        float virtualHand;
        if (distanceFromHead < threshold)
        {
            // Linear mapping within threshold
            virtualHand = distanceFromHead;
        }
        else
        {
            // Non-linear mapping beyond threshold
            float extension = k * Mathf.Pow(distanceFromHead - threshold, 2);
            virtualHand = distanceFromHead + extension;
            
            // Ensure we don't exceed maximum distance
            virtualHand = Mathf.Min(virtualHand, maxDistance);
        }

        // Calculate new position maintaining direction but with new length
        Vector3 direction = handToHead.normalized;
        Vector3 newPosition = cameraTransform.position + (direction * virtualHand);

        // Update virtual hand position and rotation
        virtualHandTransform.position = newPosition;
        virtualHandTransform.rotation = controllerTransform.rotation;
        
    }

}