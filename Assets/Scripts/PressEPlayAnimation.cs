using UnityEngine;

public class PressEPlayAnimation : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText02;
    public GameObject CoinGame;
    public Animator coinGameAnimator;

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false;

    void Update()
    {
        CheckPlayerDistance();

        if (isInRange && Input.GetKeyDown(KeyCode.E) && !minigameStarted)
        {
            StartMinigame();
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

    void StartMinigame()
    {
        Debug.Log("StartMinigame method called");
        minigameStarted = true;

        if (CoinGame != null && coinGameAnimator != null)
        {
            CoinGame.SetActive(true);
            coinGameAnimator.SetTrigger("StartMinigame");
            PauseGame();
        }
        else
        {
            Debug.LogWarning("CoinGame GameObject or Animator not assigned in the inspector");
        }
    }

    void ShowPopup()
    {
        if (popupText02 != null)
        {
            popupText02.SetActive(true);
        }
    }

    void HidePopup()
    {
        if (popupText02 != null)
        {
            popupText02.SetActive(false);
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
