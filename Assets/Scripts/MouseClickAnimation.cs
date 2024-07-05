using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayAnimationOnRightClick : MonoBehaviour, IPointerClickHandler
{
    public Sprite image01; // Das erste Bild
    public Sprite image02; // Das zweite Bild

    private Animator animator;
    private bool isPlaying = false;
    private bool isFirstAnimation = true; // Variable zum Verfolgen, welche Animation abgespielt wird
    private Image imageComponent;

    void Start()
    {
        // Animator-Komponente abrufen
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator-Komponente nicht gefunden.");
        }
        else
        {
            Debug.Log("Animator-Komponente gefunden.");
        }

        // Image-Komponente abrufen
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image-Komponente nicht gefunden.");
        }
        else
        {
            Debug.Log("Image-Komponente gefunden.");
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
            // Entweder Animation01 oder Animation02 abspielen basierend auf dem Zustand isFirstAnimation
            if (isFirstAnimation)
            {
                animator.SetBool("PlayAnimation01", true);
                Debug.Log("Playing Animation01");
                if (imageComponent != null)
                {
                    imageComponent.sprite = image02; // Bild zu image02 ändern
                    Debug.Log("Changed image to image02");
                }
                else
                {
                    Debug.LogError("Image-Komponente ist null.");
                }
            }
            else
            {
                animator.SetBool("PlayAnimation02", true);
                Debug.Log("Playing Animation02");
                if (imageComponent != null)
                {
                    imageComponent.sprite = image01; // Bild zu image01 ändern
                    Debug.Log("Changed image to image01");
                }
                else
                {
                    Debug.LogError("Image-Komponente ist null.");
                }
            }

            isPlaying = true;
            isFirstAnimation = !isFirstAnimation; // Zustand wechseln für das nächste Mal
            Debug.Log("Rechtsklick und Animation gestartet");
        }
    }

    // Methode, die aufgerufen wird, wenn die Animation beendet ist
    void OnAnimationEnd()
    {
        // Animation beenden und Zustand zurücksetzen
        animator.SetBool("PlayAnimation01", false);
        animator.SetBool("PlayAnimation02", false);
        isPlaying = false;
        Debug.Log("Animation beendet");
    }
}
