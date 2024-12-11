using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private Vector3 offset = new Vector3(0, -0.5f, 2f); // Offset from camera
    
    private Camera playerCamera;
    private bool isDisplaying = false;

    void Start()
    {
        playerCamera = Camera.main;
        notificationText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDisplaying)
        {
            // Update position to follow camera
            Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * offset.z
                                   + playerCamera.transform.up * offset.y
                                   + playerCamera.transform.right * offset.x;
            
            // Make text face the camera
            notificationText.transform.position = targetPosition;
            notificationText.transform.rotation = playerCamera.transform.rotation;
        }
    }

    public void ShowNotification(string message)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayNotification(message));
    }

    private IEnumerator DisplayNotification(string message)
    {
        isDisplaying = true;
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        notificationText.gameObject.SetActive(false);
        isDisplaying = false;
    }
}