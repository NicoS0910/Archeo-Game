using UnityEngine;
using UnityEngine.EventSystems;

public class DeactivateObjectOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject objectToDeactivate;
    public MinigameManager minigameManager;

    void Start()
    {
        if (objectToDeactivate == null)
        {
            objectToDeactivate = GameObject.Find("ObjectToDeactivate");
        }

        if (objectToDeactivate == null)
        {
            Debug.LogError($"Object to deactivate not assigned and cannot be found in scene! Problem occurred on GameObject: {gameObject.name}", this);
        }

        if (minigameManager == null)
        {
            minigameManager = FindObjectOfType<MinigameManager>();

            if (minigameManager == null)
            {
                Debug.LogWarning($"MinigameManager not found in scene! Problem occurred on GameObject: {gameObject.name}", this);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            DeactivateObject();
        }
    }

    private void DeactivateObject()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);

            if (minigameManager != null)
            {
                minigameManager.UnpauseGame();
            }
        }
        else
        {
            Debug.LogWarning($"Object to deactivate is null! Problem occurred on GameObject: {gameObject.name}", this);
        }
    }
}
