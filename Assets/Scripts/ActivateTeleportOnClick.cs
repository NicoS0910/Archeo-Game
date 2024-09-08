using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActivateTeleportOnClick : MonoBehaviour, IPointerClickHandler
{
    public int sceneBuildIndex;
    public float delayInSeconds = 2f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        LoadSceneAdditive();
    }

    private void LoadSceneAdditive()
    {
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogError("Invalid sceneBuildIndex! Please check the Build Settings.");
        }
    }
}
