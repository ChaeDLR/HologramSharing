using UnityEngine;
using Photon.Pun;
using System;

public class RoomAndSessionTest : MonoBehaviourPunCallbacks
{
    private GameObject hologram;
    private PhotonView photonView;
    private string roomName = "TestRoom";
    private string sessionName = "TestSession";
    
    // Setup method: Initialize everything for each test case.
    public void Setup()
    {
        // Create hologram and add necessary components
        hologram = new GameObject("Hologram");
        photonView = hologram.AddComponent<PhotonView>();

        // Connect to Photon and create or join a room
        PhotonNetwork.ConnectUsingSettings();
    }

    // Test case 1.5.1: Join Room
    public void TestJoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Not connected to Photon server.");
            return;
        }

        // Simulate Host Device creating a room
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Host device creating a room...");

            // Create a Photon room (if it doesn't already exist)
            PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
            Debug.Log("Room created successfully.");
        }
        else
        {
            Debug.Log("HoloLens client attempting to join room...");

            // Attempt to join the created room as a client
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    // This callback is called when joining a room or session is successful
    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined the Photon room/session.");
        // Load the shared environment or scene after joining the room/session
        PhotonNetwork.LoadLevel("SharedEnvironmentScene");
    }

    // This callback is called if there is an error joining the room or session
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room/session: {message}");
    }

    // Test case 1.5.2: Join Session
    public void TestJoinSession()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Not connected to Photon server.");
            return;
        }

        // Simulate Host Device creating a session
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Host device creating a session...");

            // Create a session (room)
            PhotonNetwork.CreateRoom(sessionName, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
            Debug.Log("Session created successfully.");
        }
        else
        {
            Debug.Log("HoloLens client attempting to join session...");

            // Attempt to join the active session
            PhotonNetwork.JoinRoom(sessionName);
        }
    }

    // Helper method to load session data and environment after joining
    private void LoadSessionDataAndEnvironment()
    {
        Debug.Log("Loading session data and environment...");
        // In a real case, load environment and synchronize session data
        // For now, simulate loading the environment scene
        PhotonNetwork.LoadLevel("SharedSessionEnvironmentScene");
    }

    // Cleanup after each test
    public void TearDown()
    {
        // Cleanup test objects and Photon connection
        if (hologram != null)
        {
            UnityEngine.Object.Destroy(hologram);
        }

        PhotonNetwork.Disconnect();
    }

    // Method to run all tests manually
    public void RunTests()
    {
        // Run Setup
        Setup();

        // Run each test case
        TestJoinRoom();
        TestJoinSession();

        // Run TearDown
        TearDown();
    }
}
