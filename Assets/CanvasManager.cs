using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject serverObject; // Referenz auf das Server-Objekt in der Szene
    public GameObject scanObject; // Referenz auf das Scan-Objekt in der Szene
    public GameObject nokiaObject; // Referenz auf das Nokia-Objekt in der Szene
    public TextMeshProUGUI serverTextMeshPro; // TextMeshProUGUI-Komponente für den Server-Text
    public TextMeshProUGUI scanTextMeshPro; // TextMeshProUGUI-Komponente für den Scan-Text
    public TextMeshProUGUI nokiaTextMeshPro; // TextMeshProUGUI-Komponente für den Nokia-Text

    private bool serverCollected = false;
    private bool scanCompleted = false;
    private bool nokiaCollected = false;

    void Start()
    {
        // Überprüfe, ob die Referenzen zugewiesen sind
        if (serverObject == null)
        {
            Debug.LogError("Server object reference is not assigned in the inspector.");
        }
        if (scanObject == null)
        {
            Debug.LogError("Scan object reference is not assigned in the inspector.");
        }
        if (nokiaObject == null)
        {
            Debug.LogError("Nokia object reference is not assigned in the inspector.");
        }
        if (serverTextMeshPro == null)
        {
            Debug.LogError("Server TextMeshProUGUI component is not assigned in the inspector.");
        }
        if (scanTextMeshPro == null)
        {
            Debug.LogError("Scan TextMeshProUGUI component is not assigned in the inspector.");
        }
        if (nokiaTextMeshPro == null)
        {
            Debug.LogError("Nokia TextMeshProUGUI component is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Überprüfe, ob der Server eingesammelt wurde
        if (!serverCollected && serverObject == null)
        {
            serverCollected = true;
            RemoveServerText();
        }

        // Überprüfe, ob das Scan-Objekt abgeschlossen wurde (Sprite "Alien_Attack_0" in der Szene)
        if (!scanCompleted && IsScanObjectCompleted())
        {
            scanCompleted = true;
            RemoveScanText();
        }

        // Überprüfe, ob das Nokia-Objekt eingesammelt wurde (GameObject mit dem Namen "NokiaQuiz" in der Szene)
        if (!nokiaCollected && GameObject.Find("NokiaQuiz") != null)
        {
            nokiaCollected = true;
            RemoveNokiaText();
        }
    }

    bool IsScanObjectCompleted()
    {
        // Prüfe, ob das Sprite "Alien_Attack_0" in der Szene erscheint
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInScene)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.sprite != null && renderer.sprite.name == "Alien_Attack_0")
            {
                return true;
            }
        }
        return false;
    }

    void RemoveServerText()
    {
        // Entferne den Server-Text aus der Szene
        if (serverTextMeshPro != null)
        {
            Destroy(serverTextMeshPro.gameObject);
        }
    }

    void RemoveScanText()
    {
        // Entferne den Scan-Text aus der Szene
        if (scanTextMeshPro != null)
        {
            Destroy(scanTextMeshPro.gameObject);
        }
    }

    void RemoveNokiaText()
    {
        // Entferne den Nokia-Text aus der Szene
        if (nokiaTextMeshPro != null)
        {
            Destroy(nokiaTextMeshPro.gameObject);
        }
    }
}
