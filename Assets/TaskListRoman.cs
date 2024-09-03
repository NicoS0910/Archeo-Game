using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListRoman : MonoBehaviour
{
    // Referenzen zu GameObjects
    public GameObject oilAmphoraObject; // Neue Aufgabe
    public GameObject puzzleGameObject; // Referenz zu PuzzleGame

    // Referenzen zu TextMeshProUGUI
    public TextMeshProUGUI romanTimes1TextMeshPro; // Neue TextMeshProUGUI
    public TextMeshProUGUI romanTimes2TextMeshPro; // Neue TextMeshProUGUI
    public TextMeshProUGUI romanTimes3TextMeshPro; // Neue TextMeshProUGUI

    // Referenzen zu Buttons
    public Button skipButton;

    // Enum zur Zustandsverwaltung
    private enum State
    {
        RomanTimes1,
        RomanTimes2,
        RomanTimes3,
        Finished
    }

    private State currentState = State.RomanTimes1;
    private bool oilAmphoraCollected = false;

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
        // Alle TextMeshProUGUI-Elemente initial verstecken
        if (romanTimes1TextMeshPro != null)
            romanTimes1TextMeshPro.gameObject.SetActive(false);
        if (romanTimes2TextMeshPro != null)
            romanTimes2TextMeshPro.gameObject.SetActive(false);
        if (romanTimes3TextMeshPro != null)
            romanTimes3TextMeshPro.gameObject.SetActive(false);

        // Zeige die erste TextMeshProUGUI
        if (romanTimes1TextMeshPro != null)
        {
            romanTimes1TextMeshPro.gameObject.SetActive(true);
        }
    }

    // Methode zum Abschluss der ersten Aufgabe
    public void CompleteOilAmphoraTask()
    {
        oilAmphoraCollected = true;
        // Wechsel zu RomanTimes2
        currentState = State.RomanTimes2;
        if (romanTimes2TextMeshPro != null)
            romanTimes2TextMeshPro.gameObject.SetActive(true);
        if (romanTimes1TextMeshPro != null)
            romanTimes1TextMeshPro.gameObject.SetActive(false);
    }

    // Methode zum Abschluss der zweiten Aufgabe
    public void CompleteRomanTimes2Task()
    {
        currentState = State.RomanTimes3;
        if (romanTimes3TextMeshPro != null)
            romanTimes3TextMeshPro.gameObject.SetActive(true);
        if (romanTimes2TextMeshPro != null)
            romanTimes2TextMeshPro.gameObject.SetActive(false);
    }

    // Methode zum Abschluss der dritten Aufgabe
    public void CompleteRomanTimes3Task()
    {
        currentState = State.Finished;
        if (romanTimes3TextMeshPro != null)
            romanTimes3TextMeshPro.gameObject.SetActive(true);
        // Skip-Button wird versteckt
        if (skipButton != null)
            skipButton.gameObject.SetActive(false);
    }

    // Handler für den Skip-Button
    void OnSkipButtonPressed()
    {
        switch (currentState)
        {
            case State.RomanTimes1:
                if (romanTimes1TextMeshPro != null)
                    romanTimes1TextMeshPro.gameObject.SetActive(false);
                
                currentState = State.RomanTimes2;
                if (romanTimes2TextMeshPro != null)
                    romanTimes2TextMeshPro.gameObject.SetActive(true);
                break;

            case State.RomanTimes2:
                if (romanTimes2TextMeshPro != null)
                    romanTimes2TextMeshPro.gameObject.SetActive(false);
                
                currentState = State.RomanTimes3;
                if (romanTimes3TextMeshPro != null)
                    romanTimes3TextMeshPro.gameObject.SetActive(true);
                break;

            case State.RomanTimes3:
                // Der Skip-Button wird hier nicht versteckt, aber sicherstellen, dass der Button nur für RomanTimes1 und RomanTimes2 sichtbar ist.
                if (skipButton != null)
                    skipButton.gameObject.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfen, ob das PuzzleGame-Objekt aktiv ist
        if (puzzleGameObject != null && puzzleGameObject.activeInHierarchy)
        {
            // Wenn RomanTimes3 sichtbar ist, verstecke es
            if (romanTimes3TextMeshPro != null && romanTimes3TextMeshPro.gameObject.activeSelf)
            {
                romanTimes3TextMeshPro.gameObject.SetActive(false);
            }
        }
    }
}
