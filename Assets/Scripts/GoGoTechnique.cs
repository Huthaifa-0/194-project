using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GoGoTechnique : MonoBehaviour
{
    [Header("GoGo Technique Parameters")]
    [Tooltip("Max distance for real hand")]
    public float Threshold = 0.3f;
    
    [Tooltip("Coefficient for non-linear scaling")]
    public float k = 0.167f;
    
    [Tooltip("Maximum allowed distance for the virtual hand")]
    public float maxDistance = 20.0f;
    
    [Header("Physical Setup")]
    [Tooltip("Vertical offset from camera to chest (usually around 0.4m)")]
    public float chestOffset = 0.4f;
    
    [Tooltip("Forward offset from camera to chest (usually around 0.1m)")]
    public float chestForwardOffset = 0.1f;
    
    [Header("References")]
    [Tooltip("Reference to the VR controller transform")]
    public Transform controllerTransform;
    
    [Tooltip("Reference to the virtual hand transform")]
    public Transform virtualHandTransform;
    
    private Vector3 chestPosition;
    private Transform cameraTransform;
    private GameObject realHandVisualizer;

    private void Start()
    {
        if (virtualHandTransform == null || controllerTransform == null)
        {
            Debug.LogError("GoGoTechnique: Required transforms not assigned!");
            enabled = false;
            return;
        }

        // Get the main camera transform (VR head position)
        cameraTransform = Camera.main.transform;
    }


    private void Update()
    {
        UpdateChestPosition();
        UpdateHandPosition();
    }

    private void UpdateChestPosition()
    {
        // Calculate chest position as offset from camera
        chestPosition = cameraTransform.position - 
                       (cameraTransform.up * chestOffset) + 
                       (cameraTransform.forward * chestForwardOffset);
    }

    private void UpdateHandPosition()
    {
        // Calculate vector from chest to real hand
        Vector3 handToChest = controllerTransform.position - chestPosition;
        float realRadius = handToChest.magnitude;

        // Calculate VirtualRadius
        float virtualRadius;
        if (realRadius < Threshold)
        {
            // Linear mapping within threshold
            virtualRadius = realRadius;
        }
        else
        {
            // Non-linear mapping beyond threshold
            virtualRadius = realRadius + k * Mathf.Pow(realRadius - Threshold, 2);
            
            // Ensure we don't exceed maximum distance
            virtualRadius = Mathf.Min(virtualRadius, maxDistance);
        }

        // Calculate new position maintaining direction but with new length
        Vector3 direction = handToChest.normalized;
        Vector3 newPosition = chestPosition + (direction * virtualRadius);

        // Update virtual hand position and rotation
        virtualHandTransform.position = newPosition;
        virtualHandTransform.rotation = controllerTransform.rotation;
        
    }

}