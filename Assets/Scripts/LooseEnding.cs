using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Für den Szenenwechsel

public class LooseEnding : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag & Drop des VideoPlayers in den Inspector
    public Button quizButton; // Drag & Drop des Quiz-Buttons in den Inspector
    public Button restartButton; // Drag & Drop des Restart-Buttons in den Inspector

    private void Start()
    {
        if (videoPlayer != null)
        {
            // Überwache, ob das Video zu Ende ist
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        if (quizButton != null)
        {
            // Initial verstecke den Button
            quizButton.gameObject.SetActive(false);
            quizButton.onClick.AddListener(QuitGame); // Füge den Listener für den Button hinzu
        }

        if (restartButton != null)
        {
            // Initial verstecke den Button
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartGame); // Füge den Listener für den Restart-Button hinzu
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Video ist zu Ende, also zeige die Buttons an
        if (quizButton != null)
        {
            quizButton.gameObject.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    private void QuitGame()
    {
        // Beende das Spiel
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Beendet das Spiel im Editor
        #else
        Application.Quit(); // Beendet das Spiel im Build
        #endif
    }

    private void RestartGame()
    {
        // Wechsle zur SampleScene
        SceneManager.LoadScene("SampleScene"); // Nimm den Namen der Szene, nicht den Pfad
    }
}
