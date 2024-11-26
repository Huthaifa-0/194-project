using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coralmanager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Color color;
    public float alpha;
    void Start()
    {
        currentHealth=0; //initial health score is 0
        alpha=1f;
        color.a=alpha; //initialize renderer as fully opaque
        UpdateMaterialTransparency(alpha);
    }

    // Update is called once per frame
    void Update(){ //change when current health is updated once colliders are implemented
        currentHealth++;
        alpha=1f - currentHealth/ maxHealth;
        UpdateMaterialTransparency(alpha);
    }

    private void UpdateMaterialTransparency(float alphaUpdated){ //method takes new alpha variable and applies it to all coral objects
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
