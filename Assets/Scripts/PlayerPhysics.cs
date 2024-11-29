using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class PlayerPhysics : MonoBehaviour
{
    private CharacterController character;
    private float fallingSpeed;
    public float gravity = -9.81f;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
    }
    
    void FixedUpdate()
    {
        // Handle gravity
        if (character.isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        // Apply gravity movement
        Vector3 movement = Vector3.up * fallingSpeed * Time.fixedDeltaTime;
        character.Move(movement);
    }
}