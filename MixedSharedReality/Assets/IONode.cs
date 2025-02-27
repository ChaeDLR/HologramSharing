using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Networked object : MonoBehaviorPun
/// Might not need to inherit from this
/// </summary>
public class IONode : MonoBehaviourPun
{
    
/// IsRoomView == true if the PhotonView was loaded with the scene(game object) or
///     instantiated with InstantiateRoomObject.
/// Start is called before the first frame update
   void Start()
    {
        /// Awake(); will FindObservables(). Called once by unity at init

        /// Need to change to OwnershipOption.Request but it cannot be set at runtime
        /// photonView.OwnershipTransfer = OwnershipOption.Fixed; PhotonView.cs: line 134
        /// implement IPunCallbacks.OnOwnershipRequest to react to the ownership request.
        /// RequestOwnership();, TransferOwnership();
        /// RPC = Remote Procedure Call. client/server coms

        /// photonView.ObservedComponents

        /// <summary>
        /// Call a RPC method of this GameObject on remote clients of this room (or on all, including this client).
        /// </summary>
        //this.photonView.RPC("MethodName", RpcTarget.All, );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UnityEvent<ManipulationEventData>
    /// </summary>
    /// <param name="coll"></param>
    public void OnCollisionEntry(Collision coll)
    {

    }

    void OnCollisionExit(Collision coll)
    {

    }

    /// <summary>
    /// PhotonView.cs: ln 602
    /// </summary>
    [PunRPC]
    void PosRemoteCB()
    {

    }
}
