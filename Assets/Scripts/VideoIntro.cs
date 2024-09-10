using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton;
    public Button backButton; // Back-Button hinzufügen
    public string nextSceneName = "SampleScene"; // Name der Szene, die nach dem letzten Video geladen werden soll
    public List<VideoClip> videoClips; // Liste der Video-Clips

    private int currentVideoIndex = 0;
    private bool skipButtonClicked = false;

    void Start()
    {
        if (videoClips.Count > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.playbackSpeed = 2.0f; // Wiedergabegeschwindigkeit auf das Doppelte setzen
            videoPlayer.Play();
        }

        skipButton.onClick.AddListener(OnSkipButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick); // Listener für den Back-Button hinzufügen

        backButton.gameObject.SetActive(false); // Back-Button zunächst deaktivieren

        // Debug-Ausgaben zur Überprüfung der Liste
        Debug.Log($"Video clips count: {videoClips.Count}");
    }

    public void OnSkipButtonClick()
    {
        if (!skipButtonClicked)
        {
            skipButtonClicked = true;
            backButton.gameObject.SetActive(true); // Back-Button beim ersten Drücken des Skip-Buttons aktivieren
        }

        PlayNextVideo();
    }

    public void OnBackButtonClick()
    {
        PlayPreviousVideo();
    }

    void PlayNextVideo()
    {
        currentVideoIndex++;

        if (currentVideoIndex < videoClips.Count)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }
        else
        {
            // Lade die nächste Szene nach dem letzten Video
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void PlayPreviousVideo()
    {
        if (currentVideoIndex > 0)
        {
            currentVideoIndex--;
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("No previous video to play.");
        }
    }
}
