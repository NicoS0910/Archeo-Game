using UnityEngine;

public class activateInfobox : MonoBehaviour
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
        CheckPlayerDistance();

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            // AchievementManager.Instance.ActivateObject("nokia_achievement", false); // Diese Zeile entfernen oder auskommentieren
            ActivateInfoBox();
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
    
    // Wenn infoBoxObject deaktiviert ist, aktiviere es
    if (!infoBoxObject.activeSelf)
    {
        infoBoxObject.SetActive(true);
    }

    // Animator vom infoBoxObject holen
    Animator animator = infoBoxObject.GetComponent<Animator>();
    if (animator != null)
    {
        // Überprüfen, ob der Animator derzeit nicht den Zustand "nokia" hat
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("nokia"))
        {
            animator.SetTrigger("Show");
        }
    }
    else
    {
        Debug.LogError("Animator component not found on infoBoxObject!");
    }
}



    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        Debug.Log("Game unpaused");

        if (playerController != null)
        {
            playerController.UnlockMovement();
        }
    }

    void DeactivateInfoBox()
    {
        if (infoBoxObject != null)
        {
            Animator animator = infoBoxObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("StopAnimation");
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
