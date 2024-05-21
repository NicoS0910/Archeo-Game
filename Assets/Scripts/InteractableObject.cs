using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText2; // Das GameObject des Popup-Texts
    public GameObject CoinGame; // Das GameObject des Minispiels, das im Editor zugewiesen werden muss

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

        // Aktiviere das CoinGame-GameObject
        if (CoinGame != null)
        {
            Debug.Log("CoinGame GameObject found: " + CoinGame.name);
            CoinGame.SetActive(true);
        }
        else
        {
            Debug.LogWarning("CoinGame GameObject not assigned in the inspector");
        }
    }

    void ShowPopup()
    {
        if (popupText2 != null)
        {
            popupText2.SetActive(true); // Aktiviere das Popup-Text-Objekt
        }
    }

    void HidePopup()
    {
        if (popupText2 != null)
        {
            popupText2.SetActive(false); // Deaktiviere den Interaktionstext
        }
    }
}
