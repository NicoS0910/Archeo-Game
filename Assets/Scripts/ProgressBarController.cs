using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progressBarFill; // Das Image, das die Füllung der ProgressBar anzeigt
    public GameObject[] objectsToMonitor; // Die Liste der Objekte, die überwacht werden sollen

    private int totalObjects;
    private int activatedObjects;

    void Start()
    {
        totalObjects = objectsToMonitor.Length;
        activatedObjects = 0;
        UpdateProgressBar(); // ProgressBar initialisieren
    }

    void Update()
    {
        CheckObjectsActivation();
    }

    void CheckObjectsActivation()
    {
        activatedObjects = 0;

        // Überprüft, ob jedes Objekt in der Liste aktiviert ist
        foreach (GameObject obj in objectsToMonitor)
        {
            if (obj.activeSelf)
            {
                activatedObjects++;
            }
        }

        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        float fillAmount = (float)activatedObjects / totalObjects;
        progressBarFill.fillAmount = fillAmount;
    }
}
