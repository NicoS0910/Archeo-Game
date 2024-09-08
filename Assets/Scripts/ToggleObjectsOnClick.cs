using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleUIObjectsOnRightClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject object1;
    public GameObject object2;

    private bool isObject1Active = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ToggleObjects();
        }
    }

    void ToggleObjects()
    {
        isObject1Active = !isObject1Active;

        object1.SetActive(isObject1Active);
        object2.SetActive(!isObject1Active);
    }
}
