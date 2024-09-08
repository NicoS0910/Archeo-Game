using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DelayedDeactivateObjectOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject objectToDeactivate;
    public MinigameManager minigameManager;
    public float delayInSeconds = 2f;

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
            StartCoroutine(DeactivateObjectWithDelay());
        }
    }

    private IEnumerator DeactivateObjectWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        DeactivateObject();
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
