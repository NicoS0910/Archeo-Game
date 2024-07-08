using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "SampleScene"; // Name der Szene, die nach dem Video geladen werden soll

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
