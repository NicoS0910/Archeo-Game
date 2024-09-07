using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArtefact : MonoBehaviour
{
    [field: SerializeField] public ToolType RequiredToolType { get; private set; }
    [SerializeField] private List<MonoBehaviour> scriptsToActivate; // Liste der zu aktivierenden Scripts
    [SerializeField] private AudioClip activationSoundClip; // Optional: Sound beim Aktivieren
    [SerializeField] private bool destroyOriginalObject = false; // Wenn true, wird das Originalobjekt zerstört, sonst deaktiviert
    private bool isActivated = false;

    private void Start()
    {
        // Deaktiviere alle hinterlegten Scripts zu Beginn
        foreach (var script in scriptsToActivate)
        {
            script.enabled = false;
        }
    }

    // Funktion zum Überprüfen, ob das richtige Werkzeug verwendet wird
    public bool TryActivate(ToolType toolType)
    {
        if (!isActivated && toolType == RequiredToolType)
        {
            StartCoroutine(Activate()); // Starte die Coroutine zur Aktivierung
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Activate()
    {
        // Sound abspielen, falls vorhanden
        if (activationSoundClip != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(activationSoundClip, transform, 1f);
        }

        // Aktiviert die hinterlegten Scripts
        foreach (var script in scriptsToActivate)
        {
            if (script != null)
            {
                script.enabled = true; // Aktiviert das Script
            }
        }

        // Falls das Originalobjekt zerstört oder deaktiviert werden soll
        if (destroyOriginalObject)
        {
            yield return new WaitForSeconds(1); // Eine Sekunde warten, bevor das Objekt zerstört wird
            Destroy(gameObject); // Zerstört das ursprüngliche Objekt
        }

        isActivated = true; // Setzt den Zustand auf "aktiviert", damit es nicht erneut ausgelöst wird
    }
}
