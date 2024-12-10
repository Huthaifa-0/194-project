using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Allows different multiplayer spawn points for the host and clients.
/// </summary>
public class PlayerPositionSpawner : MonoBehaviour
{
    private NetworkManager networkManager;

    [SerializeField]
    Vector3 hostStartPosition = Vector3.zero;

    [SerializeField]
    Vector3 clientStartPosition = Vector3.zero;

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        if (networkManager != null)
        {
            networkManager.ConnectionApprovalCallback = ApprovalCheckWithSpawnPosition;
        }
    }
    

    private void ApprovalCheckWithSpawnPosition(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        Debug.Log("Client approval: "+ request.ClientNetworkId);
        response.CreatePlayerObject = true;
        if (request.ClientNetworkId == 0){
            response.Position = hostStartPosition;
        }
        else{
            response.Position = clientStartPosition;
        }
        response.Rotation = Quaternion.identity;
        response.Approved = true;
        response.Pending = false;
    }
}
