using UnityEngine;
using UnityEngine.SceneManagement; // Notwendig, um Szenen zu wechseln

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Zuweisung des Pausenmenü-Panels im Inspektor
    private bool isPaused = false; // Status, ob das Spiel pausiert ist

    void Update()
    {
        // Prüfen, ob die Escape-Taste gedrückt wird
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape-Taste gedrückt");
            if (isPaused)
            {
                Debug.Log("Spiel wird fortgesetzt");
                Continue(); // Fortsetzen des Spiels, wenn es pausiert ist
            }
            else
            {
                Debug.Log("Spiel wird pausiert");
                Pause(); // Pausieren des Spiels, wenn es nicht pausiert ist
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true); // Aktivieren des Pausenmenü-Panels
        Time.timeScale = 0f; // Anhalten der Spielzeit
        isPaused = true; // Status auf pausiert setzen
        Debug.Log("Pausenmenü aktiviert");
    }

    public void Continue()
    {
        pausePanel.SetActive(false); // Deaktivieren des Pausenmenü-Panels
        Time.timeScale = 1f; // Fortsetzen der Spielzeit
        isPaused = false; // Status auf nicht pausiert setzen
        Debug.Log("Pausenmenü deaktiviert");
    }

    // Methode zum Beenden des Spiels
    public void QuitGame()
    {
        Debug.Log("Spiel wird beendet");
        Application.Quit(); // Beendet das Spiel (funktioniert nur in der Build-Version)
    }

    // Methode zum Wechsel ins Hauptmenü
    public void GoToMainMenu()
    {
        Debug.Log("Wechsel ins Hauptmenü");
        Time.timeScale = 1f; // Sicherstellen, dass die Zeit wieder normal läuft
        SceneManager.LoadScene("Main Menu"); // Wechselt zur Szene mit dem Namen "MainMenu"
    }
}
