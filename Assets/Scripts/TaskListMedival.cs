using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListMedieval : MonoBehaviour
{
    // Referenzen zu GameObjects
    public GameObject quizObject; // Quiz-Objekt, das die zweite Aufgabe beendet

    // Referenzen zu TextMeshProUGUI
    public TextMeshProUGUI medievalTimes1TextMeshPro; // TMP für Mittelalter 1
    public TextMeshProUGUI medievalTimes2TextMeshPro; // TMP für Mittelalter 2

    // Referenzen zu Buttons
    public Button skipButton;

    // Enum zur Zustandsverwaltung
    private enum State
    {
        MedievalTimes1,
        MedievalTimes2,
        Finished
    }

    private State currentState = State.MedievalTimes1;

    // Start is called before the first frame update
    void Start()
    {
        // Sicherstellen, dass der Skip-Button funktioniert
        if (skipButton != null)
            skipButton.onClick.AddListener(OnSkipButtonPressed);

        // Initialisiere die UI-Elemente
        InitializeUI();
    }

    // Initialisiere die UI-Elemente
    void InitializeUI()
    {
        // TMP1 wird angezeigt, TMP2 wird versteckt
        if (medievalTimes1TextMeshPro != null)
            medievalTimes1TextMeshPro.gameObject.SetActive(true);

        if (medievalTimes2TextMeshPro != null)
            medievalTimes2TextMeshPro.gameObject.SetActive(false);

        // Skip-Button wird nur bei TMP1 angezeigt
        if (skipButton != null)
            skipButton.gameObject.SetActive(true);
    }

    // Handler für den Skip-Button
    void OnSkipButtonPressed()
    {
        if (currentState == State.MedievalTimes1)
        {
            // TMP1 wird versteckt, TMP2 wird angezeigt
            if (medievalTimes1TextMeshPro != null)
                medievalTimes1TextMeshPro.gameObject.SetActive(false);

            currentState = State.MedievalTimes2;

            if (medievalTimes2TextMeshPro != null)
                medievalTimes2TextMeshPro.gameObject.SetActive(true);

            // Skip-Button wird für TMP2 deaktiviert
            if (skipButton != null)
                skipButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfen, ob das Quiz-Objekt aktiv ist
        if (quizObject != null && quizObject.activeSelf && currentState == State.MedievalTimes2)
        {
            // TMP2 wird versteckt, wenn das Quiz-Objekt aktiv wird
            if (medievalTimes2TextMeshPro != null)
                medievalTimes2TextMeshPro.gameObject.SetActive(false);

            currentState = State.Finished;
        }
    }
}
