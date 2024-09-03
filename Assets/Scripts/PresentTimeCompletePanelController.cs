using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PresentTimeCompletePanelController : MonoBehaviour
{
    public GameObject presentTimeCompletePanel; // Reference to the PresentTimeComplete Panel
    public Button completeButton; // Reference to the Button in the panel
    public Slider progressBar; // Reference to the Slider used as a loading bar

    void Start()
    {
        if (presentTimeCompletePanel == null)
        {
            Debug.LogError("PresentTimeCompletePanel reference is not assigned in the inspector.");
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

        // Hide the progress bar and deactivate the panel
        progressBar.gameObject.SetActive(false);
        if (presentTimeCompletePanel != null)
        {
            presentTimeCompletePanel.SetActive(false); // Deactivate the panel
        }
    }
}
