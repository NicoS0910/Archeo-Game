
//activateQuiz Backup


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
        //transform.localScale = Vector2.zero;
        if (popupText != null)
        {
            popupText.SetActive(false);
        }
    }

void ActivateInfoBox()
{
    // Prüfe, ob die Infobox und der Animator vorhanden sind
    if (infoBoxObject != null)
    {
        Animator animator = infoBoxObject.GetComponent<Animator>();
        if (animator != null)
        {
            // Überprüfe, ob die Animation nicht bereits abgespielt wird
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("nokia"))
            {
                animator.SetTrigger("Show"); // Trigger "Show" im Animator auslösen
            }
        }
        else
        {
            Debug.LogError("Animator component not found on infoBoxObject!");
        }

        //PauseGame(); // Spiel pausieren, wenn Info Box aktiviert wird
    }
    else
    {
        Debug.LogError("infoBoxObject is not assigned!");
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
void DeactivateInfoBox()
{
    if (infoBoxObject != null)
    {
        Animator animator = infoBoxObject.GetComponent<Animator>();
        if (animator != null)
        {
            // Hier setzen wir den Trigger zum Stoppen der Animation
            animator.SetTrigger("StopAnimation");

            // Optional: Hier könntest du sicherstellen, dass das Objekt in der gestoppten Position bleibt
            // Zum Beispiel, indem du die Position hier nicht weiter veränderst

            // Deaktiviere das Spiel pausieren, wenn die Info-Box deaktiviert wird
            UnpauseGame();
        }
        else
        {
            Debug.LogError("Animator component not found on infoBoxObject!");
        }
    }
    else
    {
        Debug.LogError("infoBoxObject is not assigned!");
    }
}

}
