using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.WSA.Sharing;

public class AnchorManager : MonoBehaviourPunCallbacks
{
    public GameObject cube; // The cube that will be anchored
    private string anchorKey = "SharedAnchor"; // Unique key for the anchor

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("[AnchorManager] Master client initializing anchor.");
            CreateAnchor();
        }
    }

    // Create and export the anchor
    private void CreateAnchor()
    {
        if (cube != null)
        {
            // Attach the world anchor to the cube
            UnityEngine.XR.WSA.WorldAnchor worldAnchor = cube.GetComponent<UnityEngine.XR.WSA.WorldAnchor>();
            if (worldAnchor == null)
            {
                worldAnchor = cube.AddComponent<UnityEngine.XR.WSA.WorldAnchor>();
            }

            // Export the anchor data to share with other clients
            WorldAnchorManager.Instance.ExportAnchor(cube, anchorKey, (byte[] data) =>
            {
                photonView.RPC("ShareAnchor", RpcTarget.Others, anchorKey, data);
            });
        }
    }

    // Sync anchor data from remote client
    [PunRPC]
    private void ShareAnchor(string key, byte[] anchorData)
    {
        Debug.Log($"[AnchorManager] Received anchor from another client: {key}");
        // Import the anchor data and attach it to the cube
        WorldAnchorManager.Instance.ImportAnchor(anchorData, cube, key);
    }
}
