using Photon.Pun;
using UnityEngine;

public class InstantOwnershipTransfer : MonoBehaviourPunCallbacks
{
    public void OnMouseDown() // or use MRTK's pointer click event for HoloLens
    {
        // Check if we are not the current owner of the object
        if (!photonView.IsMine)
        {
            // Transfer ownership instantly to the player who clicked
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
