using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton;
    public string nextSceneName = "SampleScene"; // Name der Szene, die nach dem letzten Video geladen werden soll
    public List<VideoClip> videoClips; // Liste der Video-Clips

    private int currentVideoIndex = 0;

    void Start()
    {
        if (videoClips.Count > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        skipButton.onClick.AddListener(OnSkipButtonClick);
    }

    public void OnSkipButtonClick()
    {
        PlayNextVideo();
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
}
