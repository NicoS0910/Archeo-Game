using UnityEngine;

public class FinishPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate; // Das Objekt, das aktiviert werden soll

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob der Spieler den Trigger betritt
        if (collision.CompareTag("Player"))
        {
            // Objekt aktivieren
            if (_objectToActivate != null)
            {
                _objectToActivate.SetActive(true);
            }

            // Spiel pausieren
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausiere das Spiel, indem die Zeit angehalten wird
        Debug.Log("Spiel pausiert.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Setze das Spiel fort
        Debug.Log("Spiel fortgesetzt.");
    }
}
