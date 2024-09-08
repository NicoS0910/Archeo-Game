using UnityEngine;

public class activateInfobox : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText;
    public GameObject infoBoxObject;

    private bool isInRange = false;
    private bool isGamePaused = false;

    private PlayerController playerController;

    void Start()
    {
        HidePopup();

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
        if (!infoBoxObject.activeSelf)
        {
            infoBoxObject.SetActive(true);
        }

        Animator animator = infoBoxObject.GetComponent<Animator>();
        if (animator != null)
        {
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
