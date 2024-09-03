using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObject : MonoBehaviour
{
    [field: SerializeField] public ToolType RequiredToolType { get; private set; }
    [SerializeField] private GameObject childObjectToActivate; // Das Kindobjekt, das aktiviert werden soll
    [SerializeField] private AudioClip activationSoundClip; // Optional: Sound beim Aktivieren
    [SerializeField] private bool destroyOriginalObject = true; // Wenn true, wird das Originalobjekt zerstört, sonst deaktiviert
    private bool isActivated = false;

    private void Start()
    {
        if (childObjectToActivate != null)
        {
            childObjectToActivate.SetActive(false); // Stelle sicher, dass das Kindobjekt zu Beginn deaktiviert ist
        }
    }

    public bool TryActivate(ToolType toolType)
    {
        if (!isActivated && toolType == RequiredToolType)
        {
            StartCoroutine(Activate()); // Coroutine starten
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Activate() // Methode in eine Coroutine umwandeln
    {
        if (childObjectToActivate != null)
        {
            if (activationSoundClip != null)
            {
                SoundFXManager.instance.PlaySoundFXClip(activationSoundClip, transform, 1f);
            }

            childObjectToActivate.SetActive(true); // Aktiviert das Kindobjekt
        }

        if (destroyOriginalObject)
        {
            yield return new WaitForSeconds(1); // Eine Sekunde warten
            Destroy(gameObject); // Zerstört das ursprüngliche Objekt
        }
        else
        {
            gameObject.SetActive(false); // Deaktiviert das ursprüngliche Objekt
        }

        isActivated = true; // Verhindert mehrfache Aktivierung
    }
}
