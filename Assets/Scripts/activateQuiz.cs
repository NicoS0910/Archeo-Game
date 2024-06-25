using System.Collections;
using UnityEngine;

public class activateQuiz : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText;
    public GameObject infoBoxObject; // Referenz auf das Info Box Objekt im Inspector

    private bool isInRange = false;
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus

    private PlayerController playerController; // Referenz auf den PlayerController

    void Start()
    {
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player detected in interaction range.");
            ShowPopup();
        }
        else
        {
            isInRange = false;
            HidePopup();
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            // Hier das Achievement anzeigen
            AchievementManager.Instance.ActivateObject("nokia_achievement", false);
        }

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            ActivateInfoBox();
        }
    }

    void Interact()
    {
        Debug.Log("Interacted with object");
    }

    void ShowPopup()
    {
        if (popupText != null)
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
}
