using UnityEngine;

public class ActivateSpeechbubble : MonoBehaviour
{
    public float interactionRange = 5f; // Reichweite, in der das Objekt aktiviert wird
    public GameObject objectToActivate; // Das Objekt, das aktiviert werden soll

    private bool isInRange = false;

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        // Suche nach dem Spieler-Objekt anhand des Tags "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Berechne die Distanz zwischen dem Spieler und diesem Objekt
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= interactionRange)
            {
                // Spieler ist in Reichweite, aktiviere das Objekt
                isInRange = true;
                ActivateObject();
            }
            else
            {
                // Spieler ist nicht in Reichweite, deaktiviere das Objekt
                isInRange = false;
                DeactivateObject();
            }
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null && !objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(true);
        }
    }

    void DeactivateObject()
    {
        if (objectToActivate != null && objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
