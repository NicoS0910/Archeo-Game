using UnityEngine;

public class FinishPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate; // Das Objekt, das aktiviert werden soll

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob der Spieler den Trigger betritt
        if (collision.CompareTag("Player"))
        {
            // Objekt aktivieren
            if (_objectToActivate != null)
            {
                _objectToActivate.SetActive(true);
            }
        }
    }
}
