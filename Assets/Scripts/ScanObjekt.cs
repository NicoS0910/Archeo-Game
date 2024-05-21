using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObjekt : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText; // Das GameObject des Popups
    public Sprite newSprite; // Das neue Sprite, das angezeigt werden soll

    private bool isInRange = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        HidePopup(); // Verstecke das Popup-Text-Objekt zu Beginn
    }

    void Update()
    {
        // Überprüfe, ob der Spieler in Reichweite ist
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isInRange = true;
            ShowPopup(); // Zeige das Popup-Text-Objekt an
        }
        else
        {
            isInRange = false;
            HidePopup(); // Verstecke das Popup-Text-Objekt, wenn der Spieler nicht in Reichweite ist
        }

        // Überprüfe, ob der Spieler die Interaktionstaste drückt und das Objekt in Reichweite ist
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // Ändere das Sprite, wenn ein neues Sprite vorhanden ist
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        // Deine weitere Interaktionslogik hier
        Debug.Log("Interacted with object");
    }

    void ShowPopup()
    {
        if (popupText != null)
        {
            popupText.SetActive(true); // Aktiviere das Popup-Text-Objekt
        }
    }

    void HidePopup()
    {
        if (popupText != null)
        {
            popupText.SetActive(false); // Deaktiviere das Popup-Text-Objekt
        }
    }
}
