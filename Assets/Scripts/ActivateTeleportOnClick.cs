using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ActivateTeleportOnClick : MonoBehaviour, IPointerClickHandler
{
    public int sceneBuildIndex; // Der Build-Index der neuen Szene, die geladen werden soll

    // Methode, die beim Klicken auf den Button ausgeführt wird
    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        // Überprüfen, ob der Build-Index gültig ist
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Lade die neue Szene basierend auf dem Build-Index
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Invalid sceneBuildIndex! Please check the Build Settings.");
        }
    }
}
