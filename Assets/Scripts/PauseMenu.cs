using UnityEngine;

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
}
