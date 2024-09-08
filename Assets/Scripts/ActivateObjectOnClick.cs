using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateObjectOnClick : MonoBehaviour, IPointerClickHandler
{
    private bool isGamePaused = false;

    public GameObject objectToActivate;

    void Start()
    {
        if (objectToActivate == null)
        {
            objectToActivate = GameObject.Find("ObjectToDeactivate");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate is null!");
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }
}
