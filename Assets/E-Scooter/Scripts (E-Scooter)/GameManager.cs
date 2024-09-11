using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Resource scorePoints;

    [SerializeField] private GameObject _gameOverCanvas; // Das Objekt, das aktiviert werden soll, wenn das Spiel vorbei ist

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        // Überprüfe, ob das _gameOverCanvas zugewiesen ist
        if (_gameOverCanvas != null)
        {
            if (Inventory.instance != null)
            {
                Inventory.instance.AddResources(scorePoints, -30); //Score wird reduziert
            }
            else
            {
                Debug.LogWarning("Inventory instance is missing!");
            }
            _gameOverCanvas.SetActive(true); // Aktiviere das _gameOverCanvas-Objekt
            Time.timeScale = 0f; // Pausiere das Spiel
        }
        else
        {
            Debug.LogWarning("GameOver canvas is missing!");
        }
    }
}
