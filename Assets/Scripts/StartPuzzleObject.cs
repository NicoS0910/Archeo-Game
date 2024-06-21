using UnityEngine;

public class StartPuzzleObject : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText3; // Das GameObject des Popup-Texts
    public GameObject PuzzleGame; // Das GameObject des Puzzle-Minispiels, das im Editor zugewiesen werden muss

    private bool isInRange = false;
    private bool minigameStarted = false;

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
        }
        else
        {
            Debug.LogWarning("PuzzleGame GameObject not assigned in the inspector");
        }
    }

    void ShowPopup()
    {
        if (popupText3 != null)
        {
            popupText3.SetActive(true); // Aktiviere das Popup-Text-Objekt
        }
    }

    void HidePopup()
    {
        if (popupText3 != null)
        {
            popupText3.SetActive(false); // Deaktiviere den Interaktionstext
        }
    }
}
