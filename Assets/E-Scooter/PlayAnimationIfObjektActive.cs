using UnityEngine;
using UnityEngine.UI;

namespace YourNamespace
{
    public class PlayAnimationIfObjektActive : MonoBehaviour
    {
        public GameObject targetObject; // Das spezifische Objekt, bei dem der Trigger gesetzt werden soll
        public string triggerName = "Hide"; // Der Name des Triggers im Animator

        private Animator targetAnimator; // Referenz auf den Animator-Component des Zielobjekts

        void Awake()
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

        // Methode, die aufgerufen wird, wenn das GameObject aktiviert wird
        void OnEnable()
        {
            // Verzögerung sicherstellen, damit der Animator vollständig initialisiert wird
            Invoke(nameof(TriggerAnimation), 0.1f); // Startet die Animation mit einer leichten Verzögerung
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
}
