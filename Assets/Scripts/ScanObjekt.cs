using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObjekt : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText; // Das GameObject des Popups, wenn der Server aufgenommen wurde
    public GameObject popupText1; // Das GameObject des Popups, wenn der Server noch nicht aufgenommen wurde
    public Sprite newSprite; // Das neue Sprite, das angezeigt werden soll

    private bool isInRange = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool hasServer = false; // Neue Variable, um zu überprüfen, ob der Spieler den Server hat

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

        // Überprüfe, ob der Spieler die Interaktionstaste drückt und das Objekt in Reichweite ist und den Server hat
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (hasServer)
            {
                Interact();
            }
            else
            {
                ShowPickUpServerPopup();
            }
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
        if (hasServer)
        {
            if (popupText != null)
            {
                popupText.SetActive(true); // Aktiviere das Popup-Text-Objekt
            }
        }
        else
        {
            if (popupText1 != null)
            {
                popupText1.SetActive(true); // Aktiviere das Popup-Text-Objekt, wenn der Server noch nicht aufgenommen wurde
            }
        }
    }

    void HidePopup()
    {
        if (popupText != null)
        {
            popupText.SetActive(false); // Deaktiviere das Popup-Text-Objekt
        }
        if (popupText1 != null)
        {
            popupText1.SetActive(false); // Deaktiviere das Popup-Text-Objekt, wenn der Server noch nicht aufgenommen wurde
        }
    }

    // Zeige das Popup-Text-Objekt an, wenn der Spieler versucht, mit "E" zu interagieren, bevor er den Server aufgesammelt hat
    void ShowPickUpServerPopup()
    {
        Debug.Log("Zuerst den Server aufsammeln!");
        // Hier kannst du den Popup-Text anzeigen oder eine andere Benachrichtigungsmethode verwenden
    }

    // Methode, um festzustellen, ob der Spieler den Server hat
    public void SetHasServer(bool value)
    {
        hasServer = value;
    }
}
