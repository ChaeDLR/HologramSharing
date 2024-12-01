using UnityEngine;
using Photon.Pun;

public class TransformPreservationTest : MonoBehaviour
{
    public GameObject sharedObject; // Object linked to the anchor.
    private Vector3 translation = new Vector3(1, 0, 0);
    private Quaternion rotation = Quaternion.Euler(0, 90, 0);
    private Vector3 scale = new Vector3(2, 2, 2);

    void Start()
    {
        if (PhotonNetwork.IsMasterClient) // Device A performs the transformations.
        {
            ApplyTransformations();
        }
    }

    void ApplyTransformations()
    {
        // Apply transformations.
        sharedObject.transform.position += translation;
        sharedObject.transform.rotation = rotation;
        sharedObject.transform.localScale = scale;

        // Share transformations with other devices.
        PhotonNetwork.RaiseEvent(2, new object[] { translation, rotation, scale }, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        Debug.Log("Transformations applied and shared.");
    }

    void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnTransformReceived;
    }

    void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnTransformReceived;
    }

    private void OnTransformReceived(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == 2)
        {
            object[] data = (object[])eventData.CustomData;
            Vector3 receivedTranslation = (Vector3)data[0];
            Quaternion receivedRotation = (Quaternion)data[1];
            Vector3 receivedScale = (Vector3)data[2];

            // Validate transformations.
            if (sharedObject.transform.position == receivedTranslation &&
                sharedObject.transform.rotation == receivedRotation &&
                sharedObject.transform.localScale == receivedScale)
            {
                Debug.Log("Metamorphic test passed: Transformations consistent across devices.");
            }
            else
            {
                Debug.LogError("Metamorphic test failed: Transformations not consistent.");
            }
        }
    }
}
