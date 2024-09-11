using UnityEngine;

namespace YourNamespace
{
    public class PlayAnimationIfObjektActive : MonoBehaviour
    {
        public GameObject targetObject;
        public string triggerName = "Hide";

        private Animator targetAnimator;

        void Awake()
        {
            if (targetObject == null)
                return;

            targetAnimator = targetObject.GetComponent<Animator>();
        }

        void OnEnable()
        {
            Invoke(nameof(TriggerAnimation), 0.1f);
        }

        private void TriggerAnimation()
        {
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger(triggerName);
            }
        }
    }
}
