using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetObject;
    public string triggerName = "Hide";

    private Animator targetAnimator;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("TargetObject is not assigned!");
            return;
        }

        targetAnimator = targetObject.GetComponent<Animator>();
        if (targetAnimator == null)
        {
            Debug.LogError("Animator component not found on the targetObject!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TriggerAnimation();
        }
    }

    private void TriggerAnimation()
    {
        if (targetAnimator != null)
        {
            Debug.Log("Setting trigger: " + triggerName);
            targetAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }
}
