using UnityEngine;
using Photon.Pun;

public class PositionSynchronizer : MonoBehaviourPun
{
    private Vector3 syncedPosition;
    private Quaternion syncedRotation;

    public float positionLerpSpeed = 5f;
    public float rotationLerpSpeed = 5f;

    private void Start()
    {
        if (photonView.IsMine)
        {
            Debug.Log("[CubePositioning] This client owns the cube.");
        }
        else
        {
            Debug.Log("[CubePositioning] This client does not own the cube.");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            // Smoothly interpolate position and rotation for remote clients
            transform.position = Vector3.Lerp(transform.position, syncedPosition, Time.deltaTime * positionLerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, syncedRotation, Time.deltaTime * rotationLerpSpeed);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        if (photonView.IsMine)
        {
            Debug.Log($"[CubePositioning] Setting position to {newPosition}");
            transform.position = newPosition;

            photonView.RPC("SyncPosition", RpcTarget.Others, newPosition);
        }
    }

    [PunRPC]
    private void SyncPosition(Vector3 newPosition)
    {
        Debug.Log($"[CubePositioning] Synced position: {newPosition}");
        syncedPosition = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        if (photonView.IsMine)
        {
            Debug.Log($"[CubePositioning] Setting rotation to {newRotation}");
            transform.rotation = newRotation;

            photonView.RPC("SyncRotation", RpcTarget.Others, newRotation);
        }
    }

    [PunRPC]
    private void SyncRotation(Quaternion newRotation)
    {
        Debug.Log($"[CubePositioning] Synced rotation: {newRotation}");
        syncedRotation = newRotation;
    }
}
