using UnityEngine;

public class DyonimusMinigame : MonoBehaviour
{
    public GameObject targetObject; // Das spezifische Objekt, bei dem der Trigger gesetzt werden soll
    public string triggerName = "Hide"; // Der Name des Triggers im Animator
    public GameObject[] targetObjects; // Die drei zu zerstörenden Objekte

    private int destroyedObjectsCount = 0;
    private Animator targetAnimator; // Referenz auf den Animator-Component des Zielobjekts

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
            return;
        }

        if (targetObjects.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 target objects in the inspector.");
            return;
        }

        foreach (GameObject target in targetObjects)
        {
            if (target != null)
            {
                target.AddComponent<DestroyNotifier>().onDestroyed += OnObjectDestroyed;
            }
        }
    }

    private void OnObjectDestroyed()
    {
        destroyedObjectsCount++;

        if (destroyedObjectsCount >= 3)
        {
            TriggerAnimation();
        }
    }

    private void TriggerAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }
}

public class DestroyNotifier : MonoBehaviour
{
    public delegate void DestroyedAction();
    public event DestroyedAction onDestroyed;

    private void OnDestroy()
    {
        onDestroyed?.Invoke();
    }
}
