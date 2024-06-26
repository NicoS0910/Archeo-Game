using UnityEngine;

public class StartCoinGame : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText02; // Das GameObject des Popup-Texts
    public GameObject CoinGame; // Das GameObject des Minispiels, das im Editor zugewiesen werden muss

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false; // Declare isGamePaused here

    void Update()
    {
        Debug.Log("Update method called."); // Überprüfen Sie, ob die Update-Methode ausgeführt wird.

        // Überprüfe, ob der Spieler in Reichweite ist
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isInRange = true;
            ShowPopup(); // Zeige den Interaktionstext an
            Debug.Log("Player is in range.");
        }
        else
        {
            isInRange = false;
            HidePopup(); // Verstecke den Interaktionstext
            Debug.Log("Player is out of range.");
        }

        // Überprüfe, ob der Spieler die Interaktionstaste drückt und das Objekt in Reichweite ist
        if (isInRange)
        {
            Debug.Log("Player is in interaction range."); // Bestätigen Sie, dass der Spieler in Reichweite ist.

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key was pressed."); // Bestätigen Sie, dass die E-Taste gedrückt wurde.
            }

            if (Input.GetKeyDown(KeyCode.E) && !minigameStarted)
            {
                Debug.Log("Starting the minigame."); // Bestätigen Sie, dass das Minigame gestartet wird.
                StartMinigame();
            }
        }
    }

    void StartMinigame()
    {
        Debug.Log("StartMinigame method called");
        minigameStarted = true;

        // Aktiviere das CoinGame-GameObject
        if (CoinGame != null)
        {
            Debug.Log("CoinGame GameObject found: " + CoinGame.name);
            CoinGame.SetActive(true);
            PauseGame();
        }
        else
        {
            Debug.LogWarning("CoinGame GameObject not assigned in the inspector");
        }
    }

    void ShowPopup()
    {
        if (popupText02 != null)
        {
            popupText02.SetActive(true); // Aktiviere das Popup-Text-Objekt
        }
    }

    void HidePopup()
    {
        if (popupText02 != null)
        {
            popupText02.SetActive(false); // Deaktiviere den Interaktionstext
        }
    }
        void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Spielzeit auf Null setzen, um das Spiel zu pausieren
        Debug.Log("Game paused");
    }
}
