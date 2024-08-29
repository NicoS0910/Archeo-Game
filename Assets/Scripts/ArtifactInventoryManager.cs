using UnityEngine;
using UnityEngine.UI;

public class ArtifactInventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;  // Referenz auf das Inventar UI Panel
    private bool isGamePaused = false; // Status des Spiels (pausiert oder nicht)

    void Update()
    {
        // Öffne/Schließe das Inventar und pausiere/fortsetze das Spiel, wenn 'i' gedrückt wird
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI != null)
            {
                // Inventar UI umschalten
                inventoryUI.SetActive(!inventoryUI.activeSelf);
            }

            // Spielstatus umschalten
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Methode zum Pausieren des Spiels
    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausiere das Spiel
        isGamePaused = true;
        Debug.Log("Spiel pausiert.");
    }

    // Methode zum Fortsetzen des Spiels
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Setze das Spiel fort
        isGamePaused = false;
        Debug.Log("Spiel fortgesetzt.");
    }
}
