using UnityEngine;
using Photon.Pun;

public class HologramOwnershipTests : MonoBehaviour
{
    private GameObject hologram;
    private InstantOwnershipTransfer transferOwnershipScript;
    private PhotonView photonView;

    // Setup method: Initialize everything for each test case.
    public void Setup()
    {
        // Setup test environment, instantiate hologram, and add necessary components.
        hologram = new GameObject("Hologram");
        transferOwnershipScript = hologram.AddComponent<InstantOwnershipTransfer>();
        photonView = hologram.AddComponent<PhotonView>();

        // Assuming Photon PUN is already set up for this test.
        PhotonNetwork.Instantiate("HologramPrefab", Vector3.zero, Quaternion.identity);
    }

    // Test case for transferring ownership to the local player (User A)
    public void TestOwnershipTransfer_InitialClick()
    {
        // Simulate User A clicking on the hologram.
        var localPlayer = PhotonNetwork.LocalPlayer;
        photonView.TransferOwnership(localPlayer);

        // Assert ownership has transferred to User A.
        if (photonView.Owner == localPlayer)
        {
            Debug.Log("Ownership successfully transferred to User A.");
        }
        else
        {
            Debug.LogError("Ownership transfer failed! Expected User A to own the hologram.");
        }
    }

    // Test case for repeated ownership transfer (clicking again with the same user)
    public void TestRepeatedOwnershipTransfer()
    {
        // Simulate User A clicking on the hologram.
        var localPlayer = PhotonNetwork.LocalPlayer;
        photonView.TransferOwnership(localPlayer);

        // Re-click the hologram with User A again.
        photonView.TransferOwnership(localPlayer);

        // Ownership should remain with User A.
        if (photonView.Owner == localPlayer)
        {
            Debug.Log("Ownership remains with User A, no errors.");
        }
        else
        {
            Debug.LogError("Ownership transfer failed! Ownership should remain with User A.");
        }
    }

    // Clean-up after each test
    public void TearDown()
    {
        // Cleanup test objects.
        Object.Destroy(hologram);
    }

    // Method to run all tests manually
    public void RunTests()
    {
        // Run Setup
        Setup();

        // Run each test case
        TestOwnershipTransfer_InitialClick();
        TestRepeatedOwnershipTransfer();

        // Run TearDown
        TearDown();
    }
}
