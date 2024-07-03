using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObject : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText;
    public GameObject infoBoxObject; // Referenz auf das Info Box Objekt im Inspector
    public GameObject secondaryObject; // Referenz auf das zweite Objekt im Inspector

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

        // Sicherstellen, dass das sekundäre Objekt deaktiviert ist
        if (secondaryObject != null)
        {
            secondaryObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Secondary object not assigned!");
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
            ActivateSecondaryObject();
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
            Animator animator = infoBoxObject.GetComponent<Animator>();
            if (animator != null)
            {
                // Überprüfe, ob die Animation nicht bereits abgespielt wird
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("nokia"))
                {
                    animator.SetTrigger("Show"); // Trigger "Show" im Animator auslösen
                    PauseGame();
                }
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

    void ActivateSecondaryObject()
    {
        if (secondaryObject != null)
        {
            secondaryObject.SetActive(true);
            PauseGame();
            Debug.Log("Secondary object activated");
        }
        else
        {
            Debug.LogError("Secondary object is not assigned!");
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Spielzeit auf Null setzen, um das Spiel zu pausieren
        Debug.Log("Game paused");

        if (playerController != null)
        {
            playerController.LockMovement(); // Spielerbewegung sperren
        }
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
                animator.SetTrigger("StopAnimation");

                // Spiel fortsetzen, wenn die Info Box deaktiviert wird
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
