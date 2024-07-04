using UnityEngine;
using UnityEngine.UI;

public class ButtonToStartAnimation : MonoBehaviour
{
    public GameObject infoBoxObject; // Referenz auf das GameObject mit dem Animator
    private Animator animator;

    void Start()
    {
        // Stellen Sie sicher, dass das GameObject mit dem Animator zugewiesen ist
        if (infoBoxObject != null)
        {
            animator = infoBoxObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on infoBoxObject!");
            }
        }
        else
        {
            Debug.LogError("infoBoxObject is not assigned!");
        }

        // Hier können Sie den Button registrieren, falls er im gleichen GameObject ist
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ActivateInfoBox);
        }
        else
        {
            Debug.LogError("Button component not found!");
        }
    }

    public void ActivateInfoBox()
    {
        Debug.Log("Button clicked, trying to activate info box."); // Debug-Log zum Überprüfen, ob die Methode aufgerufen wird

        // Prüfe, ob die Infobox und der Animator vorhanden sind
        if (animator != null)
        {
            // Überprüfe, ob die Animation nicht bereits abgespielt wird
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("nokia"))
            {
                Debug.Log("Triggering the 'Show' animation."); // Debug-Log zum Überprüfen, ob der Trigger gesetzt wird
                animator.SetTrigger("Show"); // Trigger "Show" im Animator auslösen
            }
            else
            {
                Debug.Log("Animation 'nokia' is already playing."); // Debug-Log, falls die Animation bereits abgespielt wird
            }
        }
        else
        {
            Debug.LogError("Animator component is missing!"); // Debug-Log, falls der Animator fehlt
        }
    }
}
