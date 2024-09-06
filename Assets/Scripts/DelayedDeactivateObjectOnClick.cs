using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DelayedDeactivateObjectOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject objectToDeactivate; // Referenz auf das zu deaktivierende Objekt
    public MinigameManager minigameManager; // Referenz auf den MinigameManager
    public float delayInSeconds = 2f; // Verzögerung in Sekunden

    void Start()
    {
        // Versuche das Standardobjekt zu finden, falls keines zugewiesen ist
        if (objectToDeactivate == null)
        {
            objectToDeactivate = GameObject.Find("ObjectToDeactivate");
        }

        // Überprüfe, ob das Objekt zugewiesen wurde
        if (objectToDeactivate == null)
        {
            Debug.LogError($"Object to deactivate not assigned and cannot be found in scene! Problem occurred on GameObject: {gameObject.name}", this);
        }

        // Finde den MinigameManager, falls nicht zugewiesen
        if (minigameManager == null)
        {
            minigameManager = FindObjectOfType<MinigameManager>();

            if (minigameManager == null)
            {
                Debug.LogWarning($"MinigameManager not found in scene! Problem occurred on GameObject: {gameObject.name}", this);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(DeactivateObjectWithDelay());
        }
    }

    private IEnumerator DeactivateObjectWithDelay()
    {
        // Warte für die angegebene Zeit
        yield return new WaitForSeconds(delayInSeconds);

        // Objekt deaktivieren
        DeactivateObject();
    }

    private void DeactivateObject()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Deaktiviere das Objekt

            // Wenn ein MinigameManager zugewiesen ist, rufe die Methode auf, um das Spiel fortzusetzen
            if (minigameManager != null)
            {
                minigameManager.UnpauseGame();
            }
        }
        else
        {
            Debug.LogWarning($"Object to deactivate is null! Problem occurred on GameObject: {gameObject.name}", this);
        }
    }
}
