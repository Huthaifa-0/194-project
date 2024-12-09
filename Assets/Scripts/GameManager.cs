using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private float currentScore = 0f;
    private float currentHealth = 0f;
    private float maxHealth = 100f; 

    public Color color;
    public float alpha;
    void initializecoralcolor()
    {
    alpha = 1f;

    color.a=alpha; //initialize renderer as fully opaque
    UpdateMaterialTransparency();

    }


    // Method to be assigned in Unity Events for updating score
    public void UpdateScore()
    {
        currentScore++; 
    }

    // Method to be assigned in Unity Events for updating health
    public void UpdateHealth()
    {
        currentHealth++;
        
        // Update health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }

        // Update health text
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth:F0}/{maxHealth:F0} %";
        }
    }
    public void UpdateMaterialTransparency(){
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
    [Rpc(SendTo.ClientsAndHost)]
    public void sendMessage(string message){

    }

    public void HealthChecker(){
        if (currentHealth >= 50 ){
            SendMessage("Congrats You are half way through !! ");
        }
    }

}