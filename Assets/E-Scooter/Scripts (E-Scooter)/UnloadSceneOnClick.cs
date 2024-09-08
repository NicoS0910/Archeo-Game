using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UnloadSceneOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int sceneBuildIndex; // Der Build-Index der Szene, die entladen werden soll

    // Diese Methode wird aufgerufen, wenn auf das UI-Element geklickt wird
    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UnloadScene();
        }
    }

    // Entlädt die Szene asynchron
    private void UnloadScene()
    {
        // Überprüfen, ob der Build-Index der Szene gültig ist
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);

            // Überprüfen, ob die Szene geladen ist
            if (scene.isLoaded)
            {
                Debug.Log($"Entlade Szene mit Index {sceneBuildIndex}: {scene.name}");
                SceneManager.UnloadSceneAsync(sceneBuildIndex);
            }
            else
            {
                Debug.LogWarning($"Die Szene mit Index {sceneBuildIndex} ist nicht geladen.");
            }
        }
        else
        {
            Debug.LogError("Ungültiger SceneBuildIndex! Bitte überprüfe die Build-Einstellungen.");
        }
    }
}
