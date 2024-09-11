using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UnloadSceneOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int sceneBuildIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UnloadScene();
        }
    }

    private void UnloadScene()
    {
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);

            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneBuildIndex);
            }
        }
    }
}
