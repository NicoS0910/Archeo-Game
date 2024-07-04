using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayAnimationOnRightClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    private Animator animator;
    private bool isPlaying = false;

    void Start()
    {
        // Animator-Komponente abrufen
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator-Komponente nicht gefunden.");
        }

        // Event hinzufügen, das ausgelöst wird, wenn die Animation beendet ist
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.functionName = "OnAnimationEnd";
            animationEvent.time = clip.length;
            clip.AddEvent(animationEvent);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Prüfen, ob es sich um einen Rechtsklick handelt und die Animation nicht bereits abgespielt wird
        if (eventData.button == PointerEventData.InputButton.Right && !isPlaying)
        {
            // Animation abspielen
            animator.SetBool("PlayAnimation", true);
            isPlaying = true;
            Debug.Log("Rechtsklick und Animation gestartet");
        }
    }

    // Methode, die aufgerufen wird, wenn die Animation beendet ist
    void OnAnimationEnd()
    {
        // Animation beenden und Zustand zurücksetzen
        animator.SetBool("PlayAnimation", false);
        isPlaying = false;
        Debug.Log("Animation beendet und zu DoNothing zurückgekehrt");

        // Deaktiviere ein Objekt und aktiviere ein anderes
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
            Debug.Log("Objekt deaktiviert: " + objectToDeactivate.name);
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log("Objekt aktiviert: " + objectToActivate.name);
        }
    }
}
