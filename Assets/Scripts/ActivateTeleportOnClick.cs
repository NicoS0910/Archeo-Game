using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActivateTeleportOnClick : MonoBehaviour, IPointerClickHandler
{
    public int sceneBuildIndex; // Der Build-Index der neuen Szene, die geladen werden soll
    public float delayInSeconds = 2f; // Verzögerung in Sekunden

    // Methode, die beim Klicken auf den Button ausgeführt wird
    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Warte für die angegebene Verzögerung
        yield return new WaitForSeconds(delayInSeconds);

        // Szene additiv laden
        LoadSceneAdditive();
    }

    private void LoadSceneAdditive()
    {
        // Überprüfen, ob der Build-Index gültig ist
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Lade die neue Szene additiv basierend auf dem Build-Index
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogError("Invalid sceneBuildIndex! Please check the Build Settings.");
        }
    }
}
