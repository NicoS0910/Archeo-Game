using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetObject; // Das spezifische Objekt, bei dem der Trigger gesetzt werden soll
    public string triggerName = "Hide"; // Der Name des Triggers im Animator

    private Animator targetAnimator; // Referenz auf den Animator-Component des Zielobjekts

    void Start()
    {
        // Sicherstellen, dass ein Zielobjekt zugewiesen wurde
        if (targetObject == null)
        {
            Debug.LogError("TargetObject is not assigned!");
            return;
        }

        // Holen des Animator-Components vom Zielobjekt
        targetAnimator = targetObject.GetComponent<Animator>();
        if (targetAnimator == null)
        {
            Debug.LogError("Animator component not found on the targetObject!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TriggerAnimation();
        }
    }

    private void TriggerAnimation()
    {
        if (targetAnimator != null)
        {
            // Trigger im Animator des Zielobjekts setzen, um die Animation zu starten
            Debug.Log("Setting trigger: " + triggerName);
            targetAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }
}
