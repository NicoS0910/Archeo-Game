using UnityEngine;

public class StartPuzzleGame : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText03; // Das GameObject des Popup-Texts
    public GameObject PuzzleGame; // Das GameObject des Minispiels, das im Editor zugewiesen werden muss

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus


    void Update()
    {
        // Überprüfe, ob der Spieler in Reichweite ist
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isInRange = true;
            ShowPopup(); // Zeige den Interaktionstext an
        }
        else
        {
            isInRange = false;
            HidePopup(); // Verstecke den Interaktionstext
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
}