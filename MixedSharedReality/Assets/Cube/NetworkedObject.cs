using Photon.Pun;
using UnityEngine;

public class NetworkedCube : MonoBehaviourPun
{
    private void Start()
    {
        if (!photonView.IsMine)
        {
            // Disable user interactions for non-owning clients
            GetComponent<Collider>().enabled = false;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Optionally, add any local transformations (e.g., drag or move the cube)
            // We sync position for shared state across devices
            transform.position = new Vector3(Mathf.Sin(Time.time), 1f, 0f);
        }
    }

    public void SyncCubePosition(Vector3 newPosition)
    {
        photonView.RPC("RPC_UpdatePosition", RpcTarget.All, newPosition);
    }

    [PunRPC]
    public void RPC_UpdatePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
