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

    public void GameOver()
    {
        if (_gameOverCanvas != null)
        {
            if (Inventory.instance != null)
            {
                Inventory.instance.AddResources(scorePoints, -30);
            }

            _gameOverCanvas.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }
}
