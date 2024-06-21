using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeactivateObjectOnRightClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject objectToDeactivate; // Referenz auf das zu deaktivierende Objekt

    void Start()
    {
        // Wähle das Standardobjekt, falls keines zugewiesen ist
        if (objectToDeactivate == null)
        {
            objectToDeactivate = GameObject.Find("ObjectToDeactivate");
        }

        // Überprüfe, ob das Objekt zugewiesen wurde
        if (objectToDeactivate == null)
        {
            Debug.LogError("Object to deactivate not assigned and cannot be found in scene!");
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
            objectToDeactivate.SetActive(false); // Deaktiviere das Objekt
        }
        else
        {
            Debug.LogWarning("Object to deactivate is null!");
        }
    }
}
