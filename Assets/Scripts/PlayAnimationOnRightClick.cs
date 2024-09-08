using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YourNamespace
{
    public class PlayAnimationOnRightClick : MonoBehaviour, IPointerClickHandler
    {
        public Sprite image01;
        public Sprite image02;

        private Animator animator;
        private bool isPlaying = false;
        private bool isFirstAnimation = true;
        private Image imageComponent;

        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator-Komponente nicht gefunden.");
            }
            else
            {
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            }

            imageComponent = GetComponent<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("Image-Komponente nicht gefunden.");
            }

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
            if (eventData.button == PointerEventData.InputButton.Right && !isPlaying)
            {
                if (isFirstAnimation)
                {
                    animator.SetBool("PlayAnimation01", true);
                    imageComponent.sprite = image02;
                }
                else
                {
                    animator.SetBool("PlayAnimation02", true);
                    imageComponent.sprite = image01;
                }

                isPlaying = true;
                isFirstAnimation = !isFirstAnimation;
            }
        }

        void OnAnimationEnd()
        {
            animator.SetBool("PlayAnimation01", false);
            animator.SetBool("PlayAnimation02", false);
            isPlaying = false;
        }
    }
}
