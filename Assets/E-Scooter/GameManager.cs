using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private int sceneBuildIndex; // Build-Index der Szene, die neu gestartet werden soll

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
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Überprüfen, ob der Build-Index der Szene gültig ist
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneBuildIndex); // Laden der Szene basierend auf dem angegebenen Build-Index
        }
        else
        {
            Debug.LogError("Invalid sceneBuildIndex! Please check the Build Settings.");
        }
    }
}
