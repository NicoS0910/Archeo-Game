using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctTag;      // Der korrekte Tag für das fallen gelassene Objekt
    private MinigameManager minigameManager;

    void Awake()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
        if (minigameManager == null)
        {
            Debug.LogError("MinigameManager not found in the scene.");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped Object: " + droppedObject.name);
            if (!string.IsNullOrEmpty(droppedObject.tag))
            {
                if (droppedObject.CompareTag(correctTag))
                {
                    Debug.Log("Correct Tag: " + correctTag);
                    droppedObject.transform.SetParent(transform);
                    droppedObject.transform.position = transform.position;
                    droppedObject.transform.rotation = Quaternion.identity; // Setzt die Rotation zurück
                    minigameManager.CheckCoins();  // Überprüft die Münzen im Minispielmanager
                    minigameManager.CheckPieces();
                }
                else
                {
                    Debug.Log("Incorrect Tag: " + droppedObject.tag);
                }
            }
            else
            {
                Debug.Log("Tag is null or empty.");
            }
        }
    }
}
