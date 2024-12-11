using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using JetBrains.Annotations;
using System.Collections.Generic;


public class GameManager : NetworkBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider beachHealthBar;
    [SerializeField] private Slider seaHealthBar;
    [SerializeField] private TextMeshProUGUI beachHealthText;
    [SerializeField] private TextMeshProUGUI seaHealthText;

    private float currentScore = 0f;
     private TMP_Text prgressMessage;
    private NetworkVariable<float> beachCurrentHealth = new NetworkVariable<float>(0f);
    private NetworkVariable<float> seaCurrentHealth = new NetworkVariable<float>(0f);
    private float maxHealth = 30f;

    //public Transform environmentObjects;

    public Color color;
    public float alpha;
    public List<GameObject> PlantObjectsList = new List<GameObject>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        beachCurrentHealth.OnValueChanged += OnBeachHealthChanged;
        seaCurrentHealth.OnValueChanged += OnSeaHealthChanged;

    }


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

    public void UpdateMaterialTransparency(){

        // How would we take the health score from another class . can we make it public static to access it 
        alpha= 1f - beachCurrentHealth.Value / maxHealth;

        //method takes new alpha variable and applies it to all coral objects
        //optimize by updating trasparency only when health score changes
        //updating every frame can make it laggy
        foreach (GameObject child in PlantObjectsList){
            Renderer plantRenderer = child.GetComponent<Renderer>();
            Material overlayMaterial = plantRenderer.materials[1];
            Color color = overlayMaterial.color;
            color.a = alpha; // Update transparency
            overlayMaterial.color = color;
        } 
    }

    void OnBeachHealthChanged(float oldValue, float newValue){ //change from transform to gameobject
        if(beachCurrentHealth.Value >= 50){
            prgressMessage.text = "Congrats you are half way through!";
        }

         // Update health bar
        if (beachHealthBar != null)
        {
            beachHealthBar.value = beachCurrentHealth.Value / maxHealth;
        }

        // Update health text
        if (beachHealthText != null)
        {
            beachHealthText.text = $"Health: {beachCurrentHealth:F0}/{maxHealth:F0} %";
        } 
         // How would we take the health score from another class . can we make it public static to access it 
        alpha= 1f - beachCurrentHealth.Value / maxHealth;

        //method takes new alpha variable and applies it to all coral objects
        //optimize by updating trasparency only when health score changes
        //updating every frame can make it laggy
        foreach (GameObject child in PlantObjectsList){
            Renderer coralRenderer = child.GetComponent<Renderer>();
            if (coralRenderer != null){
                Material overlayMaterial = coralRenderer.materials[1];
                Color color = overlayMaterial.color;
                color.a = alpha; // Update transparency
                overlayMaterial.color = color;
            }
            
                
                
        }
    }
    void OnSeaHealthChanged(float oldValue, float newValue){
        if(seaCurrentHealth.Value >= 50){
            prgressMessage.text = "Congrats you are half way through!";
        }

         // Update health bar
        if (beachHealthBar != null)
        {
            beachHealthBar.value = beachCurrentHealth.Value / maxHealth;
        }

        // Update health text
        if (beachHealthText != null)
        {
            beachHealthText.text = $"Health: {beachCurrentHealth:F0}/{maxHealth:F0} %";
        } 
         // How would we take the health score from another class . can we make it public static to access it 
        alpha= 1f - beachCurrentHealth.Value / maxHealth;

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

    [Rpc(SendTo.Server)]
    public void UpdateBeachHealthRpc(){
        if (!IsServer) return;
        beachCurrentHealth.Value++;
    }
    [Rpc(SendTo.Server)]
    public void UpdateSeaHealthRpc(){
        if (!IsServer) return;
        seaCurrentHealth.Value++;
    }
}