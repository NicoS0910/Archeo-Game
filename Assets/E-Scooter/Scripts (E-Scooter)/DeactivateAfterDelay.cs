using UnityEngine;

public class DeactivateAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 5f; // Zeit in Sekunden bis zur Deaktivierung

    void Start()
    {
        // Plant die Deaktivierung des Objekts nach der angegebenen Verzögerung
        Invoke("DeactivateObject", delay);
    }

    private void DeactivateObject()
    {
        // Deaktiviert das GameObject
        gameObject.SetActive(false);
    }
}
