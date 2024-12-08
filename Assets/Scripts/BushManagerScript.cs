using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushManager : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    private Color color;
    private float alpha;

    void Start()
    {
        currentHealth=0; //initial health score is 0
        alpha=1f;
        color.a=alpha; //initialize renderer as fully opaque
        UpdateMaterialTransparency(alpha);
    }

    
    void Update()
    {
        
    }
    private void UpdateMaterialTransparency(float alphaUpdated){
        currentHealth++;
        // How would we take the health score from another class . can we make it public static to access it 
        alpha= 1f - currentHealth/ maxHealth;

        //method takes new alpha variable and applies it to all coral objects
        //optimize by updating trasparency only when health score changes
        //updating every frame can make it laggy
        foreach (Transform child in transform){
            Renderer coralRenderer = child.GetComponent<Renderer>();
            if (coralRenderer != null){
                Material overlayMaterial = coralRenderer.materials[1];
                Color color = overlayMaterial.color;
                color.a = alpha; // Update transparency
                overlayMaterial.color = color;
            }      
        }
    }
}
