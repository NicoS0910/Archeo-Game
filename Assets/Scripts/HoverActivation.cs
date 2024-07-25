using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject targetObject;  // Das Objekt, das aktiviert/deaktiviert werden soll
    [SerializeField] private bool activateOnHover = true;  // Ob das Objekt beim Hover aktiviert werden soll

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
            targetObject.SetActive(true);  // Zielobjekt aktivieren
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject != null && activateOnHover)
        {
            targetObject.SetActive(false);  // Zielobjekt deaktivieren
        }
    }
}
