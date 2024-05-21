using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctCoinTag; // Setze dies im Inspector auf den richtigen Tag (z.B. "Coin1", "Coin2", "Coin3")
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
        if (droppedObject != null && droppedObject.CompareTag(correctCoinTag))
        {
            droppedObject.transform.SetParent(transform);
            droppedObject.transform.position = transform.position;
            minigameManager.CheckCoins(); // Überprüfe, ob alle Münzen korrekt platziert sind
        }
    }
}
