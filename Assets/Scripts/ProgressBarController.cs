using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progressBarFill;
    public GameObject[] objectsToMonitor;

    private int totalObjects;
    private int activatedObjects;

    void Start()
    {
        totalObjects = objectsToMonitor.Length;
        activatedObjects = 0;
        UpdateProgressBar();
    }

    void Update()
    {
        CheckObjectsActivation();
    }

    void CheckObjectsActivation()
    {
        activatedObjects = 0;

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
