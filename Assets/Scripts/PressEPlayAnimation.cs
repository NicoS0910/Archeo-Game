using UnityEngine;

public class PressEPlayAnimation : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject popupText02; // Das GameObject des Popup-Texts
    public GameObject CoinGame; // Das GameObject des Minispiels, das im Editor zugewiesen werden muss
    public Animator coinGameAnimator; // Der Animator für die Animation des Minispiels

    private bool isInRange = false;
    private bool minigameStarted = false;
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
