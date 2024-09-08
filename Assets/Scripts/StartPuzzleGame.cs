using UnityEngine;

public class StartPuzzleGame : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText03;
    public GameObject PuzzleGame;
    public LayerMask playerLayer;

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false;

    void Update()
    {
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

        if (isInRange && Input.GetKeyDown(KeyCode.E) && !minigameStarted)
        {
            StartMinigame();
        }
    }

    void StartMinigame()
    {
        Debug.Log("StartMinigame method called");
        minigameStarted = true;

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
        if (popupText03 != null)
        {
            popupText03.SetActive(true);
        }
    }

    void HidePopup()
    {
        if (popupText03 != null)
        {
            popupText03.SetActive(false);
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
