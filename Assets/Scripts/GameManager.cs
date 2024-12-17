using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq.Expressions;
using UnityEngine.PlayerLoop;



public class GameManager : NetworkBehaviour
{
    public UnityEvent notification = new UnityEvent();
    [Header("UI References")]
    [SerializeField] private Slider beachHealthBar;
    [SerializeField] private Slider seaHealthBar;
    public TextMeshProUGUI scoreText;

    private float currentScore = 0f;

    private TMP_Text progressMessage;
    private NetworkVariable<float> beachCurrentHealth = new NetworkVariable<float>(0f);
    private NetworkVariable<float> seaCurrentHealth = new NetworkVariable<float>(0f);
    private float maxHealth = 20f;

    //public Transform environmentObjects;

    public Color color;
    public float alpha;
    public List<GameObject> PlantObjectsList = new List<GameObject>();
    private NotificationSystem notificationSystem;
    private AudioSource audioSource;
    public AudioClip notificationSound ;

    void Start()
    {
        // Get reference to notification system when script starts
        notificationSystem = FindObjectOfType<NotificationSystem>();
        audioSource = GetComponent<AudioSource>();

        // notification.Invoke();
        // PlayNotificationSound();


    }


    public void ProgressNotification()
    {
        // Show notification when event happens
        notificationSystem.ShowNotification("Congrats you are half way through!");
    }

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
        scoreText.GetComponent<TMP_Text>().text = currentScore.ToString();
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

        if(currentScore >= 10){
            notification.Invoke();
            PlayNotificationSound();
        }



         // Update health bar
        if (beachHealthBar != null)
        {
            beachHealthBar.value = beachCurrentHealth.Value;
        }

        // Update health text
        // if (beachHealthText != null)
        // {
        //     beachHealthText.text = $"Health: {beachCurrentHealth:F0}/{maxHealth:F0} %";
        // } 
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
        if(currentScore >= 10){
            notification.Invoke();
            PlayNotificationSound();
        }


         // Update health bar
        // if (beachHealthBar != null)
        // {
        //     beachHealthBar.value = beachCurrentHealth.Value / maxHealth;
        // }

        // // Update health text
        // if (beachHealthText != null)
        // {
        //     beachHealthText.text = $"Health: {beachCurrentHealth:F0}/{maxHealth:F0} %";
        // } 
         // How would we take the health score from another class . can we make it public static to access it 
        alpha= 1f - beachCurrentHealth.Value / maxHealth; //should this be relying on seaHealth?

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
    //Method to play a sound 
    public void PlayNotificationSound()
    {
        audioSource.PlayOneShot(notificationSound);
    }
    //Roc updatethe beach and sea health scores and update the value of the slider bar in the hand menu

    [Rpc(SendTo.Server)]
    public void UpdateBeachHealthRpc(){
        //if (!IsServer) return;
        beachCurrentHealth.Value++;
        Debug.Log(beachCurrentHealth);
        
        
        // beachHealthBar.value = beachCurrentHealth.Value;
        // seaHealthBar.value = seaCurrentHealth.Value ;


    }
    [Rpc(SendTo.Server)]
    public void UpdateSeaHealthRpc(){
        //if (!IsServer) return;
        seaCurrentHealth.Value++;
        Debug.Log(seaCurrentHealth);


        // seaHealthBar.value = seaCurrentHealth.Value ;
        // beachHealthBar.value = beachCurrentHealth.Value;

    }
}
