using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool activateOnHover = true;

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject != null && activateOnHover)
        {
            targetObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject != null && activateOnHover)
        {
            targetObject.SetActive(false);
        }
    }
}
