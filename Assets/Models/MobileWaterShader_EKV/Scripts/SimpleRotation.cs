using UnityEngine;
using UnityEngine.UI;

namespace MobileWaterShaderEKV
{

[ExecuteInEditMode]
public class SimpleRotation : MonoBehaviour
{
    public int rotationsPerMinute;
    public float xFactor, yFactor, zFactor;

    public void SetRPM(float f)              
    {                                        
            rotationsPerMinute = Mathf.RoundToInt(f);          
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xFactor * rotationsPerMinute * (Time.deltaTime),
                        yFactor * rotationsPerMinute * (Time.deltaTime),
                        zFactor * rotationsPerMinute * (Time.deltaTime));
    }

}
}
