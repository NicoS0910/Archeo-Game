using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ActivateObjectInScene : MonoBehaviour, IPointerClickHandler
{
    public int targetSceneBuildIndex; // Der Build-Index der Szene, die das Objekt enthält
    public string objectNameInTargetScene; // Der Name des Objekts, das aktiviert werden soll

    // Methode, die beim Klicken auf den Button ausgeführt wird
    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ActivateObjectInLoadedScene();
        }
    }

    private void ActivateObjectInLoadedScene()
    {
        // Überprüfen, ob der Build-Index der Zielszene gültig ist
        if (targetSceneBuildIndex >= 0 && targetSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Hole die Zielszene anhand des Build-Index
            Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex);

            // Überprüfen, ob die Zielszene bereits geladen ist
            if (targetScene.isLoaded)
            {
                // Durchlaufe alle GameObjects in der Szene und suche nach dem spezifischen Objekt
                bool objectFound = false;
                foreach (GameObject rootObject in targetScene.GetRootGameObjects())
                {
                    if (rootObject.name == objectNameInTargetScene)
                    {
                        rootObject.SetActive(true); // Aktiviere das Objekt
                        Debug.Log($"Object '{objectNameInTargetScene}' in the target scene has been activated.");
                        objectFound = true;
                        break; // Verlasse die Schleife, wenn das Objekt gefunden wurde
                    }
                }

                if (!objectFound)
                {
                    Debug.LogError($"Object '{objectNameInTargetScene}' not found in the target scene!");
                }
            }
            else
            {
                Debug.LogError($"Target scene with index {targetSceneBuildIndex} is not loaded! Please make sure the target scene is loaded before trying to activate the object.");
            }
        }
        else
        {
            Debug.LogError("Invalid targetSceneBuildIndex! Please check the Build Settings.");
        }
    }
}
