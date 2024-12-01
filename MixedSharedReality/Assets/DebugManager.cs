using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Text debugText; // Assign in the Inspector (or use TextMeshPro's TMP_Text)

    private string messages = "";

    // Singleton instance for easy global access
    public static DebugManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Log(string message)
    {
        messages += message + "\n";

        // Trim the message log to avoid excessive memory usage
        if (messages.Length > 1000)
        {
            messages = messages.Substring(messages.Length - 1000);
        }

        if (debugText != null)
        {
            debugText.text = messages;
        }
    }
}
