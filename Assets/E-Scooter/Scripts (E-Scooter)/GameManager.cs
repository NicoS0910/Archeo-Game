using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Resource scorePoints;
    [SerializeField] private GameObject _gameOverCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    // Start-Methode, die direkt beim Laden des Minigame-Scripts ausgeführt wird
    private void Start()
    {
        // Stelle sicher, dass der spezielle Minigame-Song sofort abgespielt wird
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayScooterMinigameSong();
        }
    }

    // Methode, um das Scooter-Minispiel zu beenden und zur normalen Playlist zurückzukehren
    public void EndScooterMinigame()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayRegularPlaylist();
        }
    }

    public void GameOver()
    {
        if (_gameOverCanvas != null)
        {
            if (Inventory.instance != null)
            {
                Inventory.instance.AddResources(scorePoints, -30);
            }
            EndScooterMinigame();
            _gameOverCanvas.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }
}
