using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


public class ContinousMovement : MonoBehaviour
{
    public XRNode inputSource;

    private XROrigin rig;  // Changed from XROrigin to XROrigin3D

    private Vector2 inputAxis;
    public float speed = 1f;
    private CharacterController character;
    private Camera mainCamera; // Reference to the VR camera

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
        // Get the camera reference
        mainCamera = rig.Camera;
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    void FixedUpdate()
    {
        // Use the camera's Y rotation instead of the rig's
        Quaternion headYaw = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        
        // Normalize the direction to prevent faster diagonal movement
        if (direction.magnitude > 0.1f)
        {
            direction = direction.normalized;
        }

        character.Move(Time.fixedDeltaTime * speed * direction);
    }
}
