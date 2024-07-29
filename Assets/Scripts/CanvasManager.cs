using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject serverObject;
    public GameObject scanObject;
    public GameObject nokiaObject;
    public TextMeshProUGUI serverTextMeshPro;
    public TextMeshProUGUI scanTextMeshPro;
    public TextMeshProUGUI nokiaTextMeshPro;
    public TextMeshProUGUI introTextMeshPro;
    public TextMeshProUGUI intro2TextMeshPro;
    public TextMeshProUGUI intro3TextMeshPro;
    public TextMeshProUGUI intro4TextMeshPro;
    public Button skipButton;

    private enum State
    {
        Intro1,
        Intro2,
        ServerTask,
        Intro3,
        ScanTask,
        Intro4,
        NokiaTask,
        Finished
    }

    private State currentState = State.Intro1;
    private bool serverCollected = false;
    private bool scanCompleted = false;
    private bool nokiaCollected = false;

    void Start()
    {
        // Überprüfe, ob die Referenzen zugewiesen sind
        CheckReferences();

        // Zeige den Intro-Text und den Skip-Button an, verstecke die anderen Texte
        ShowIntroText();
        HideIntro2Text();
        HideIntro3Text();
        HideIntro4Text();
        HideTaskTexts();

        // Füge die OnClick-Methode des Skip-Buttons hinzu
        skipButton.onClick.AddListener(OnSkipButtonPressed);

        // Initialisiere den Skip-Button Status
        UpdateSkipButtonState();
    }

    void Update()
    {
        // Überprüfe, ob die aktuelle Aufgabe abgeschlossen ist, wenn wir im Aufgabenbereich sind
        switch (currentState)
        {
            case State.ServerTask:
                if (!serverCollected && serverObject == null)
                {
                    serverCollected = true;
                    HideServerText();
                    currentState = State.Intro3;
                    ShowIntro3Text();
                    UpdateSkipButtonState();
                }
                break;
            case State.ScanTask:
                if (!scanCompleted && IsScanObjectCompleted())
                {
                    scanCompleted = true;
                    HideScanText();
                    currentState = State.Intro4;
                    ShowIntro4Text();
                    UpdateSkipButtonState();
                }
                break;
            case State.NokiaTask:
                if (!nokiaCollected && GameObject.Find("NokiaQuiz") != null)
                {
                    nokiaCollected = true;
                    HideNokiaText();
                    currentState = State.Finished;
                    // Alle Aufgaben abgeschlossen
                }
                break;
        }
    }

    void CheckReferences()
    {
        if (serverObject == null)
            Debug.LogError("Server object reference is not assigned in the inspector.");
        if (scanObject == null)
            Debug.LogError("Scan object reference is not assigned in the inspector.");
        if (nokiaObject == null)
            Debug.LogError("Nokia object reference is not assigned in the inspector.");
        if (serverTextMeshPro == null)
            Debug.LogError("Server TextMeshProUGUI component is not assigned in the inspector.");
        if (scanTextMeshPro == null)
            Debug.LogError("Scan TextMeshProUGUI component is not assigned in the inspector.");
        if (nokiaTextMeshPro == null)
            Debug.LogError("Nokia TextMeshProUGUI component is not assigned in the inspector.");
        if (introTextMeshPro == null)
            Debug.LogError("Intro TextMeshProUGUI component is not assigned in the inspector.");
        if (intro2TextMeshPro == null)
            Debug.LogError("Intro2 TextMeshProUGUI component is not assigned in the inspector.");
        if (intro3TextMeshPro == null)
            Debug.LogError("Intro3 TextMeshProUGUI component is not assigned in the inspector.");
        if (intro4TextMeshPro == null)
            Debug.LogError("Intro4 TextMeshProUGUI component is not assigned in the inspector.");
        if (skipButton == null)
            Debug.LogError("Skip Button reference is not assigned in the inspector.");
    }

    void ShowIntroText()
    {
        if (introTextMeshPro != null)
        {
            introTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(introTextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntroText()
    {
        if (introTextMeshPro != null)
            introTextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro2Text()
    {
        if (intro2TextMeshPro != null)
        {
            intro2TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro2TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro2Text()
    {
        if (intro2TextMeshPro != null)
            intro2TextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro3Text()
    {
        if (intro3TextMeshPro != null)
        {
            intro3TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro3TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro3Text()
    {
        if (intro3TextMeshPro != null)
            intro3TextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro4Text()
    {
        if (intro4TextMeshPro != null)
        {
            intro4TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro4TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro4Text()
    {
        if (intro4TextMeshPro != null)
            intro4TextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowServerText()
    {
        if (serverTextMeshPro != null)
        {
            serverTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(serverTextMeshPro));
        }
    }

    void HideServerText()
    {
        if (serverTextMeshPro != null)
            serverTextMeshPro.gameObject.SetActive(false);
    }

    void ShowScanText()
    {
        if (scanTextMeshPro != null)
        {
            scanTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(scanTextMeshPro));
        }
    }

    void HideScanText()
    {
        if (scanTextMeshPro != null)
            scanTextMeshPro.gameObject.SetActive(false);
    }

    void ShowNokiaText()
    {
        if (nokiaTextMeshPro != null)
        {
            nokiaTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(nokiaTextMeshPro));
        }
    }

    void HideNokiaText()
    {
        if (nokiaTextMeshPro != null)
            nokiaTextMeshPro.gameObject.SetActive(false);
    }

    void HideTaskTexts()
    {
        HideServerText();
        HideScanText();
        HideNokiaText();
    }

    void UpdateSkipButtonState()
    {
        if (currentState == State.Intro1 || currentState == State.Intro2 || 
            currentState == State.Intro3 || currentState == State.Intro4)
        {
            if (skipButton != null)
                skipButton.gameObject.SetActive(true);
        }
        else
        {
            if (skipButton != null)
                skipButton.gameObject.SetActive(false);
        }
    }

    public void OnSkipButtonPressed()
    {
        switch (currentState)
        {
            case State.Intro1:
                currentState = State.Intro2;
                HideIntroText();
                ShowIntro2Text();
                break;
            case State.Intro2:
                currentState = State.ServerTask;
                HideIntro2Text();
                ShowServerText();
                break;
            case State.ServerTask:
                currentState = State.Intro3;
                HideServerText();
                ShowIntro3Text();
                break;
            case State.Intro3:
                currentState = State.ScanTask;
                HideIntro3Text();
                ShowScanText();
                break;
            case State.ScanTask:
                currentState = State.Intro4;
                HideScanText();
                ShowIntro4Text();
                break;
            case State.Intro4:
                currentState = State.NokiaTask;
                HideIntro4Text();
                ShowNokiaText();
                break;
            case State.NokiaTask:
                currentState = State.Finished;
                HideNokiaText();
                // Alle Aufgaben abgeschlossen
                break;
        }

        UpdateSkipButtonState();
    }

    IEnumerator StartTypingEffect(TextMeshProUGUI textMeshPro)
    {
        TypingEffect typingEffect = textMeshPro.GetComponent<TypingEffect>();
        if (typingEffect != null)
        {
            yield return typingEffect.TypeText(textMeshPro.text);
        }
    }

    bool IsScanObjectCompleted()
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInScene)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.sprite != null && renderer.sprite.name == "Auto1.1")
            {
                return true;
            }
        }
        return false;
    }
}
