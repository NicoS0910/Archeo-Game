using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RomanTimeCompletePanelController : MonoBehaviour
{
    public GameObject romanTimeCompletePanel; // Referenz zum Roman Time Complete Panel
    public Button completeButton; // Referenz zum Button im Panel
    public Slider progressBar; // Referenz zur Fortschrittsleiste
    public GameObject medievalTasklistPanel; // Referenz zum Medieval Tasklist Panel

    void Start()
    {
        if (romanTimeCompletePanel == null)
        {
            Debug.LogError("RomanTimeCompletePanel reference is not assigned in the inspector.");
            return;
        }

        if (completeButton == null)
        {
            Debug.LogError("Complete Button reference is not assigned in the inspector.");
            return;
        }

        if (progressBar == null)
        {
            Debug.LogError("Progress Bar reference is not assigned in the inspector.");
            return;
        }

        if (medievalTasklistPanel == null)
        {
            Debug.LogError("Medieval Tasklist Panel reference is not assigned in the inspector.");
            return;
        }

        // Initialize the progress bar
        progressBar.gameObject.SetActive(false);

        // Add listener to the button
        completeButton.onClick.AddListener(OnCompleteButtonPressed);
    }

    void OnCompleteButtonPressed()
    {
        StartCoroutine(UploadProcess());
    }

    IEnumerator UploadProcess()
    {
        // Show the progress bar
        progressBar.gameObject.SetActive(true);

        // Simulate the upload process with a loading bar
        float elapsedTime = 0f;
        float duration = 3f; // Simulate a 3-second upload process

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            progressBar.value = progress; // Update the progress bar
            yield return null;
        }

        // Hide the progress bar and deactivate the Roman Time Complete Panel
        progressBar.gameObject.SetActive(false);
        if (romanTimeCompletePanel != null)
        {
            romanTimeCompletePanel.SetActive(false); // Deactivate the panel
        }

        // Activate the Medieval Tasklist Panel
        if (medievalTasklistPanel != null)
        {
            medievalTasklistPanel.SetActive(true);
        }
    }
}
