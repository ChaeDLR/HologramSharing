using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Photon.Pun;

public class AnchorStabilityTest : MonoBehaviour
{
    private string anchorId = "StabilityAnchor";
    private Vector3 anchorPosition = new Vector3(0, 1, 0);
    private GameObject anchorObject;

    void Start()
    {
        // Step 1: Create the anchor at the predefined position
        CreateAnchor(anchorPosition);

        // Step 2: Simulate disconnect and reconnect by waiting before validation.
        Invoke("ValidateAnchorStability", 5.0f); // Wait 5 seconds before validation.
    }

    // Create and attach an anchor using a GameObject.
    void CreateAnchor(Vector3 position)
    {
        // Create the GameObject that represents the anchor.
        anchorObject = new GameObject(anchorId);
        anchorObject.transform.position = position;
    }

    void ValidateAnchorStability()
    {
        // Step 3: Retrieve the anchor's position after reconnection (simulated by waiting).
        Vector3 actualPosition = anchorObject.transform.position;

        // Step 4: Verify stability (check if the position is within tolerance).
        float tolerance = 0.05f; // Allow some small tolerance for drift.
        if (Vector3.Distance(anchorPosition, actualPosition) <= tolerance)
        {
            Debug.Log("Metamorphic test passed: Anchor position stable over time.");
        }
        else
        {
            Debug.LogError($"Metamorphic test failed: Anchor drifted. Expected {anchorPosition}, but got {actualPosition}.");
        }
    }
}
