using UnityEngine;

[ExecuteInEditMode]
public class FoamController : MonoBehaviour
{
    public float oscillationPeriod = 4.0f; // Time in seconds for one full oscillation.
    public float minValue = -1.0f;         // Minimum value.
    public float maxValue = 1.0f;          // Maximum value.

    private float timer = 0.0f;
    private Material material; // Reference to the material using the shader.

    [SerializeField]
    float mappedValue;

    [SerializeField]
    float oscillationValue;

    private void Start()
    {
        // Get the material from the renderer component.
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.sharedMaterial;
    }

    private void Update()
    {
        // Update the timer.
        timer += Time.deltaTime;

        // Calculate the sine of the current time to oscillate between -1 and 1.
        oscillationValue = Mathf.Sin((timer / oscillationPeriod) * 2 * Mathf.PI);

        // Map the oscillationValue to your desired range.
        mappedValue = Mathf.Lerp(minValue, maxValue, (oscillationValue + 1) / 2);

        // Set the mappedValue to the shader property.
        material.SetFloat("_FoamDir", mappedValue);
    }
}
