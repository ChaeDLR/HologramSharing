using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;
using Photon.Pun;

public class AnchorConsistencyTest : MonoBehaviour
{
    private Vector3 testPosition = new Vector3(0, 1, 0);
    private Transform anchorTransform;
    private GameObject anchorObject;

    void Start()
    {
        // Initialize Anchor at a fixed position.
        CreateAnchor(testPosition);

        // If this device is the master client, share the anchor position with others.
        if (PhotonNetwork.IsMasterClient)
        {
            ShareAnchorPosition();
        }
    }

    // Create and attach an anchor.
    void CreateAnchor(Vector3 position)
    {
        // Create an empty GameObject to represent the anchor.
        anchorObject = new GameObject("Anchor");
        anchorObject.transform.position = position;

        // You can use MRTK's MixedRealitySpatialAwarenessSystem to handle spatial anchor management
        var spatialAnchorSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySpatialAwarenessSystem>();
        if (spatialAnchorSystem == null)
        {
            Debug.LogError("Spatial Awareness System is not initialized.");
            return;
        }

        // Optionally, you could create a spatial anchor here, but for simplicity, we are using a GameObject.
        anchorTransform = anchorObject.transform;
    }

    // Share anchor position across devices using Photon.
    void ShareAnchorPosition()
    {
        // Share the position of the anchor object with other devices via Photon.
        Vector3 anchorPosition = anchorObject.transform.position;
        PhotonNetwork.RaiseEvent(1, anchorPosition, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnAnchorPositionReceived;
    }

    void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnAnchorPositionReceived;
    }

    // This method is called when another device receives the anchor position.
    private void OnAnchorPositionReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == 1)
        {
            Vector3 receivedPosition = (Vector3)eventData.CustomData;

            // Compare received position with expected position for consistency.
            float tolerance = 0.01f;
            if (Vector3.Distance(receivedPosition, testPosition) <= tolerance)
            {
                Debug.Log("Metamorphic test passed: Anchor position consistent across devices.");
            }
            else
            {
                Debug.LogError($"Metamorphic test failed: Expected {testPosition}, but got {receivedPosition}.");
            }
        }
    }
}
