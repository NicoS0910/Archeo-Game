using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ActivateObjectInScene : MonoBehaviour, IPointerClickHandler
{
    public int targetSceneBuildIndex;
    public string objectNameInTargetScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ActivateObjectInLoadedScene();
        }
    }

    private void ActivateObjectInLoadedScene()
    {
        if (targetSceneBuildIndex >= 0 && targetSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex);

            if (targetScene.isLoaded)
            {
                bool objectFound = false;
                foreach (GameObject rootObject in targetScene.GetRootGameObjects())
                {
                    if (rootObject.name == objectNameInTargetScene)
                    {
                        rootObject.SetActive(true);
                        objectFound = true;
                        break;
                    }
                }
            }
        }
    }
}
