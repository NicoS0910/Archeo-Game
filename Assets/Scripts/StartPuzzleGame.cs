using UnityEngine;

public class StartPuzzleGame : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText03; // Das GameObject des Popup-Texts
    public GameObject PuzzleGame; // Das GameObject des Minispiels, das im Editor zugewiesen werden muss
    public LayerMask playerLayer; // Layer für den Spieler

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus

    void Update()
    {
        // Überprüfen, ob der Spieler im Interaktionsbereich ist
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, playerLayer);
        isInRange = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                isInRange = true;
                ShowPopup();
                break;
            }
        }

        if (!isInRange)
        {
            HidePopup();
        }

        // Überprüfe, ob der Spieler die Interaktionstaste drückt und das Objekt in Reichweite ist
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !minigameStarted)
        {
            StartMinigame();
        }
    }

    void StartMinigame()
    {
        Debug.Log("StartMinigame method called");
        minigameStarted = true;

        // Aktiviere das PuzzleGame-GameObject
        if (PuzzleGame != null)
        {
            Debug.Log("PuzzleGame GameObject found: " + PuzzleGame.name);
            PuzzleGame.SetActive(true);
            PauseGame(); // Spiel pausieren, wenn das Minispiel gestartet wird
        }
        else
        {
            Debug.LogWarning("PuzzleGame GameObject not assigned in the inspector");
        }
    }

    void ShowPopup()
    {
        if (popupText03 != null)
        {
            popupText03.SetActive(true); // Aktiviere das Popup-Text-Objekt
        }
    }

    void HidePopup()
    {
        if (popupText03 != null)
        {
            popupText03.SetActive(false); // Deaktiviere den Interaktionstext
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Spielzeit auf Null setzen, um das Spiel zu pausieren
        Debug.Log("Game paused");
    }

    void OnDrawGizmosSelected()
    {
        // Zeichne eine visuelle Darstellung des Interaktionsbereichs im Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
