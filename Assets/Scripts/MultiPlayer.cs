using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Events;


public class MultiPlayer : NetworkBehaviour
{
   public const int MAX_PLAYER_AMOUNT = 2;
   private const string KEY_RELAY_JOIN_CODE = "RelayJoinCode";

   public static MultiPlayer Instance { get; private set; }

   [SerializeField]
   private UnityEvent OnHostStartedEvent;
  
   [SerializeField]
   private UnityEvent OnClientStartedEvent;

   private Lobby joinedLobby;

   private void Awake() {
       // If there is an instance, and it's not me, delete myself.
       if (Instance != null && Instance != this)
       {
           Destroy(this);
           return;
       }
       else
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }


       InitializeUnityAuthentication();
   }

   /// <summary>
   /// Initializes the Unity services and signs in anonymously
   /// </summary>
   private async void InitializeUnityAuthentication() {
       if (UnityServices.State != ServicesInitializationState.Initialized) {
           InitializationOptions initializationOptions = new InitializationOptions();
           initializationOptions.SetProfile("Player"+Random.Range(0, 100).ToString()); // Set the Player Name. Need the random range because players on the same computer for debugging need different names.


           await UnityServices.InitializeAsync(initializationOptions);


           await AuthenticationService.Instance.SignInAnonymouslyAsync();
       }
   }

   public async void CreateLobby(string lobbyName, bool isPrivate=false) {
       try {
           joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, MAX_PLAYER_AMOUNT, new CreateLobbyOptions {
               IsPrivate = isPrivate,
           });


           // Heartbeat the lobby every 15 seconds.
           StartCoroutine(HeartbeatLobbyCoroutine(joinedLobby.Id, 15));


           Allocation allocation = await RelayService.Instance.CreateAllocationAsync(MAX_PLAYER_AMOUNT-1);


           string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
          
           NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
           NetworkManager.Singleton.StartHost();


           await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions {
               Data = new Dictionary<string, DataObject> {
                    { KEY_RELAY_JOIN_CODE , new DataObject(DataObject.VisibilityOptions.Member, relayJoinCode) }
                }
           });


           OnHostStartedEvent.Invoke();
          
       } catch (LobbyServiceException e) {
           Debug.Log(e);
       }
   }

   public async void QuickJoin() {
       try {
           joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();


           string relayJoinCode = joinedLobby.Data[KEY_RELAY_JOIN_CODE].Value;
           Debug.Log("Join Code: "+relayJoinCode);


           JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);


           NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
           NetworkManager.Singleton.StartClient();
           OnClientStartedEvent.Invoke();


       } catch (LobbyServiceException e) {
           Debug.Log(e);
       }
   }

   IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
   {
       var delay = new WaitForSecondsRealtime(waitTimeSeconds);


       while (true)
       {
           LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
           yield return delay;
       }
   }

   void OnApplicationQuit()
   {
       DeleteLobby();
   }

   public async void DeleteLobby() {
       if (joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId) {
           try {
               await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);


               joinedLobby = null;
           } catch (LobbyServiceException e) {
               Debug.Log(e);
           }
       }
   }
}
