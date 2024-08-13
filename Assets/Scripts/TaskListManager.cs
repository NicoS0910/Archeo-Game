using UnityEngine;
using UnityEngine.UI;

public class TaskListManager : MonoBehaviour
{
    public GameObject taskListPanel; // Das Panel, das die Task-Liste enthält
    public GameObject anrufPanel; // Das Panel für den Anruf
    public Button acceptButton; // Der Accept-Button
    public Button declineButton; // Der Decline-Button
    public AudioClip missionSound; // Der Sound, der bei jedem Anruf abgespielt wird
    public float retryInterval = 2.0f; // Zeitintervall in Sekunden, bis der Anruf erneut angezeigt wird

    private AudioSource audioSource; // AudioSource zum Abspielen von Sounds
    private bool isWaitingForResponse = false; // Zeigt an, ob wir auf eine Antwort warten

    void Start()
    {
        // Überprüfe, ob die Panels und Buttons zugewiesen sind
        if (taskListPanel == null)
        {
            Debug.LogError("TaskListPanel is not assigned.");
        }
        else
        {
            // Stelle sicher, dass die Task-Liste zu Beginn nicht sichtbar ist
            taskListPanel.SetActive(false);
        }

        if (anrufPanel == null)
        {
            Debug.LogError("AnrufPanel is not assigned.");
        }

        if (acceptButton == null)
        {
            Debug.LogError("AcceptButton is not assigned.");
        }

        if (declineButton == null)
        {
            Debug.LogError("DeclineButton is not assigned.");
        }

        if (missionSound == null)
        {
            Debug.LogError("MissionSound is not assigned.");
        }

        // Füge eine AudioSource-Komponente hinzu, wenn sie noch nicht vorhanden ist
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Setze den AudioClip der AudioSource-Komponente
        audioSource.clip = missionSound;

        // Listener für die Buttons hinzufügen
        acceptButton.onClick.AddListener(AcceptCall);
        declineButton.onClick.AddListener(DeclineCall);

        // Das Anruf-Panel initial anzeigen
        ShowCallPanel();
    }

    void Update()
    {
        // Überprüfe, ob die Space-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Wenn das Task-Liste-Panel sichtbar ist, verstecke es
            if (taskListPanel.activeSelf)
            {
                taskListPanel.SetActive(false);
            }
            // Ansonsten, zeige es nur, wenn das Anruf-Panel nicht sichtbar ist
            else if (!anrufPanel.activeSelf)
            {
                taskListPanel.SetActive(true);
            }
        }
    }

    void ShowCallPanel()
    {
        // Zeigt das Anruf-Panel an, setzt die Warte-Flagge auf true und spielt den Typung-Sound ab
        anrufPanel.SetActive(true);
        isWaitingForResponse = true;
        audioSource.Play(); // Spielt den Typung-Sound ab
    }

    void AcceptCall()
    {
        // Verstecke das Anruf-Panel und zeige das Task-Liste-Panel
        anrufPanel.SetActive(false);
        taskListPanel.SetActive(true);
        isWaitingForResponse = false; // Setze die Warte-Flagge zurück
    }

    void DeclineCall()
    {
        // Verstecke das Anruf-Panel und starte den Mechanismus, um es nach einem Intervall erneut anzuzeigen
        anrufPanel.SetActive(false);
        isWaitingForResponse = false; // Setze die Warte-Flagge zurück
        Invoke(nameof(ShowCallPanel), retryInterval); // Rufe die Methode nach dem angegebenen Intervall erneut auf
    }
}
