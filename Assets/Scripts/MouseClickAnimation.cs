using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAnimationOnRightClick : MonoBehaviour, IPointerClickHandler
{
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
        animator.SetBool("PlayAnimation", false);
        isPlaying = false;
        Debug.Log("Animation beendet und zu DoNothing zurückgekehrt");
    }
}
