using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManagerScript : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1.0"; // Set the game version
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 10000; // Increased timeout
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.SentCountAllowance = 7; // Increased retries
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon!");
        PhotonNetwork.JoinOrCreateRoom("HoloRoom", new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined the room!");
        // Instantiate objects here (cube, etc.)
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined: " + newPlayer.NickName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected from Photon due to: {0}", cause);
    }
}
