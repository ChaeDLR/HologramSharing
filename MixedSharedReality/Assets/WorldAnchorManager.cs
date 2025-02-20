using UnityEngine;
using UnityEngine.XR.WSA.Sharing;
using UnityEngine.XR.WSA;
using System.Collections.Generic;


public class WorldAnchorManager : MonoBehaviour
{
    private static WorldAnchorManager _instance;
    public static WorldAnchorManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Export a WorldAnchor
    public void ExportAnchor(GameObject anchorObject, string anchorKey, System.Action<byte[]> onExportComplete)
{
    WorldAnchor anchor = anchorObject.GetComponent<WorldAnchor>();
    if (anchor == null)
    {
        Debug.LogError("No WorldAnchor found on the object.");
        return;
    }

    // Create a transfer batch and add the WorldAnchor
    WorldAnchorTransferBatch transferBatch = new WorldAnchorTransferBatch();
    
        if (!transferBatch.AddWorldAnchor(anchorKey, anchor))
        {
            Debug.Log($"Failed to add WorldAnchor with anchorKey={anchorKey}.\nWorldAnchorManager.cs, Ln: 37\n");
        }

    Debug.Log($"Starting anchor export for key: {anchorKey}");

    // List to hold the serialized data chunks
    List<byte> serializedData = new List<byte>();

    // Start the asynchronous export process
    WorldAnchorTransferBatch.ExportAsync(transferBatch,
        (byte[] data) =>
        {
            // Collect data chunks as they become available
            serializedData.AddRange(data);
        },
        (SerializationCompletionReason reason) => 
        {
            // Check if the export succeeded
            if (reason == SerializationCompletionReason.Succeeded)
            {
                Debug.Log($"Anchor export succeeded for key: {anchorKey}");
                onExportComplete?.Invoke(serializedData.ToArray()); // Invoke callback with complete data
            }
            else
            {
                Debug.LogError($"Anchor export failed for key: {anchorKey} with reason: {reason}");
            }
        });
}


    // Import a WorldAnchor
   public void ImportAnchor(byte[] data, GameObject anchorObject, string anchorKey)
    {
        if (data == null || data.Length == 0)
        {
            Debug.LogError("Invalid anchor data: Data is null or empty.");
            return;
        }

        Debug.Log($"Starting anchor import for key: {anchorKey}. Data size: {data.Length} bytes.");

        WorldAnchorTransferBatch.ImportAsync(data, (SerializationCompletionReason reason, WorldAnchorTransferBatch batch) =>
        {
            if (reason == SerializationCompletionReason.Succeeded)
            {
                if (batch == null)
                {
                    Debug.LogError("Import succeeded, but the WorldAnchorTransferBatch is null.");
                    return;
                }

                // Attempt to lock the anchor to the object
                WorldAnchor anchor = batch.LockObject(anchorKey, anchorObject);
                if (anchor != null)
                {
                    Debug.Log($"Anchor successfully imported and locked to object: {anchorKey}");
                }
                else
                {
                    Debug.LogError($"Failed to lock anchor to object. Anchor key '{anchorKey}' may not exist in the batch.");
                }
            }
            else
            {
                Debug.LogError($"Failed to import anchor. Reason: {reason}");
            }
        });
    }

}
