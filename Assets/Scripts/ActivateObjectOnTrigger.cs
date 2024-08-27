using UnityEngine;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    public GameObject targetObject; // Das Objekt, das aktiviert werden soll
    public string playerTag = "Player"; // Der Tag, der dem Spieler zugewiesen ist

    void Start()
    {
        // Sicherstellen, dass das Zielobjekt zugewiesen wurde
        if (targetObject == null)
        {
            Debug.LogError("TargetObject is not assigned!");
        }
        else
        {
            targetObject.SetActive(false); // Das Zielobjekt initial deaktivieren
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Spieler den Trigger betritt
        if (other.CompareTag(playerTag))
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Zielobjekt aktivieren
            Debug.Log("TargetObject activated!");
        }
    }
}
