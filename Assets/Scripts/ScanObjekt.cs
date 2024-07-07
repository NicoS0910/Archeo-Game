using UnityEngine;

public class ScanObjekt : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText;
    public GameObject infoBoxObject; // Referenz auf das Info Box Objekt im Inspector
    public Sprite newSprite; // Hier das neue Sprite im Inspector zuweisen

    private bool isInRange = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool hasServer = false;
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus

    private PlayerController playerController; // Referenz auf den PlayerController

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        HidePopup();

        // PlayerController Komponente finden
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }
    }

    void Update()
    {
        CheckPlayerDistance();

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (hasServer)
            {
                Interact();
                // Hier das Achievement anzeigen
                AchievementManager.Instance.ActivateObject("scan_achievement", false);
            }
            else
            {
                ShowPickUpServerPopup();
            }
        }

        // Check if 'A' key is pressed
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            ActivateInfoBox();
        }

        // Check if 'S' key is pressed for scanning (neue Aufgabe)
        if (isInRange && Input.GetKeyDown(KeyCode.S))
        {
            ScanObject();
        }
    }

    void CheckPlayerDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= interactionRange)
            {
                isInRange = true;
                ShowPopup();
            }
            else
            {
                isInRange = false;
                HidePopup();
            }
        }
    }

    void Interact()
    {
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            Debug.Log("Sprite geändert");
        }
        else
        {
            Debug.LogWarning("Kein neues Sprite zugewiesen!");
        }
        Debug.Log("Interacted with object");
    }

    void ShowPopup()
    {
        if (hasServer && popupText != null)
        {
            popupText.SetActive(true);
        }
    }

    void HidePopup()
    {
        if (popupText != null)
        {
            popupText.SetActive(false);
        }
    }

    void ShowPickUpServerPopup()
    {
        Debug.Log("Zuerst den Server aufsammeln!");
    }

    public void SetHasServer(bool value)
    {
        hasServer = value;
    }

    void ActivateInfoBox()
    {
        if (infoBoxObject != null)
        {
            infoBoxObject.SetActive(true);
            PauseGame(); // Spiel pausieren, wenn Info Box aktiviert wird
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Spielzeit auf Null setzen, um das Spiel zu pausieren
        Debug.Log("Game paused");
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Spielzeit auf Eins setzen, um das Spiel fortzusetzen
        Debug.Log("Game unpaused");

        if (playerController != null)
        {
            playerController.UnlockMovement(); // Spielerbewegung wieder erlauben
        }
    }

    // Methode zum Deaktivieren des Info Box Objekts
    public void DeactivateInfoBox()
    {
        if (infoBoxObject != null)
        {
            infoBoxObject.SetActive(false);
            UnpauseGame(); // Sicherstellen, dass das Spiel wieder fortgesetzt wird
        }
    }

    // Neue Methode zum Scannen des Objekts (zweite Aufgabe)
    void ScanObject()
    {
        Debug.Log("Object scanned!");
        // Hier kannst du weitere Aktionen ausführen, wenn das Objekt gescannt wird
        // Zum Beispiel: Setze einen Status, zeige eine Benachrichtigung an usw.
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
