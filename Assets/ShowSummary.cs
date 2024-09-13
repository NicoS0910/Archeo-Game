using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ShowSummary : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // Referenz zur VideoPlayer-Komponente
    public Button summaryButton;         // Referenz zum Summary-Button
    public Button quitButton;            // Referenz zum Quit-Button
    public GameObject summaryPanel;      // Referenz zum Summary-Text-Panel
    public Button backButton;            // Referenz zum Back-Button

    private void Start()
    {
        // Stelle sicher, dass die Buttons zu Beginn deaktiviert sind
        summaryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Stelle sicher, dass das Summary-Panel zu Beginn deaktiviert ist
        summaryPanel.SetActive(false);

        // Füge Event-Listener hinzu, um zu reagieren, wenn das Video endet
        videoPlayer.loopPointReached += OnVideoEnd;

        // Füge Event-Listener für die Buttons hinzu
        summaryButton.onClick.AddListener(ShowSummaryPanel);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(HideSummaryPanel);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Aktiviere den Summary-Button und den Quit-Button
        summaryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private void ShowSummaryPanel()
    {
        // Verstecke die Summary- und Quit-Buttons
        summaryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Zeige das Summary-Panel an
        summaryPanel.SetActive(true);
    }

    private void HideSummaryPanel()
    {
        // Verstecke das Summary-Panel
        summaryPanel.SetActive(false);

        // Zeige die Summary- und Quit-Buttons wieder an
        summaryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    // Methode zum Beenden des Spiels, die dem Quit-Button zugewiesen wird
    private void QuitGame()
    {
        Application.Quit();
    }
}
