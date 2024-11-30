using UnityEngine;

public class FollowUser : MonoBehaviour
{
    public Transform userCamera; // Assign the Camera Transform in the Inspector

    void Update()
    {
        Vector3 forward = userCamera.forward;
        forward.y = 0; // Keep the panel level
        transform.position = userCamera.position + forward * 0.5f; // Adjust distance
        transform.rotation = Quaternion.LookRotation(forward);
    }
}
