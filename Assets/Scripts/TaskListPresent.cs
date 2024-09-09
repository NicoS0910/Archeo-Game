using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListPresent : MonoBehaviour
{
    public GameObject serverObject;
    public GameObject scanObject;
    public GameObject nokiaObject;
    public GameObject nutellaObject;
    public GameObject scarfObject;
    public GameObject costumeObject;
    public RawImage serverPicture;
    public TextMeshProUGUI serverTextMeshPro;
    public TextMeshProUGUI scanTextMeshPro;
    public TextMeshProUGUI nokiaTextMeshPro;
    public TextMeshProUGUI introTextMeshPro;
    public TextMeshProUGUI intro2TextMeshPro;
    public TextMeshProUGUI intro3TextMeshPro;
    public TextMeshProUGUI nutellaTextMeshPro;
    public TextMeshProUGUI eScooterTextMeshPro;
    public TextMeshProUGUI intro5TextMeshPro;
    public TextMeshProUGUI costumeTextMeshPro;
    public TextMeshProUGUI scarfTextMeshPro;
    public Button skipButton;
    public Button nokiaQuizStartButton;
    public Button eScooterButton;
    public Button hideEScooterButton;
    public Button finishButton;

    public AudioSource audioSource;
    public AudioClip typingSound;

    public Canvas canvas;
    public GameObject panel;
    public GameObject presentTimeCompletePanel;
    public GameObject taskListPanel;

    private enum State
    {
        Intro1,
        Intro2,
        Intro3,
        ServerTask,
        ScanTask,
        NokiaTask,
        NutellaTask,
        E_ScooterTask,
        Finished
    }

    private State currentState = State.Intro1;
    private bool serverCollected = false;
    private bool scanCompleted = false;
    private bool nokiaCollected = false;
    private bool nutellaCollected = false;

    private bool nutellaTextActivated = false;
    private bool nokiaTextActivated = false;
    private bool scarfTextActivated = false;
    private bool costumeTextActivated = false;

    private bool nokiaObjectActivated = false;
    private bool nutellaObjectActivated = false;
    private bool scarfObjectActivated = false;
    private bool costumeObjectActivated = false;

    void Start()
    {
        CheckReferences();

        if (canvas != null)
            canvas.gameObject.SetActive(false);

        if (panel != null)
            panel.SetActive(false);

        if (presentTimeCompletePanel != null)
            presentTimeCompletePanel.SetActive(false);

        serverPicture.gameObject.SetActive(false);
        serverTextMeshPro.gameObject.SetActive(false);
        scanTextMeshPro.gameObject.SetActive(false);
        eScooterTextMeshPro.gameObject.SetActive(false);
        intro5TextMeshPro.gameObject.SetActive(false);
        costumeTextMeshPro.gameObject.SetActive(false);
        scarfTextMeshPro.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(false);

        if (eScooterButton != null)
            eScooterButton.onClick.AddListener(OnEScooterButtonClicked);

        if (hideEScooterButton != null)
            hideEScooterButton.onClick.AddListener(OnHideEScooterButtonClicked);

        if (finishButton != null)
            finishButton.onClick.AddListener(OnFinishButtonClicked);

        Invoke("ActivateCanvasAndPanel", 3f);
    }

    void ActivateCanvasAndPanel()
    {
        canvas?.gameObject.SetActive(true);
        panel?.gameObject.SetActive(true);
        StartCoroutine(InitializeUI());
    }

    IEnumerator InitializeUI()
    {
        ShowIntroText();
        HideIntro2Text();
        HideIntro3Text();
        HideTaskTexts();

        skipButton.onClick.AddListener(OnSkipButtonPressed);
        nokiaQuizStartButton.onClick.AddListener(OnNokiaQuizStartButtonPressed);
        UpdateSkipButtonState();
        yield return null;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.ServerTask:
                if (!serverCollected && serverObject == null)
                {
                    serverCollected = true;
                    HideServerText();
                    HideServerPicture();
                    currentState = State.ScanTask;
                    ShowScanText();
                }
                break;
            case State.ScanTask:
                if (!scanCompleted && IsScanObjectCompleted())
                {
                    scanCompleted = true;
                    HideScanText();
                    currentState = State.NokiaTask;
                    ShowNokiaText();
                    UpdateSkipButtonState();
                }
                else if (scanObject != null && scanObject.activeSelf)
                {
                    HideScanText();
                }
                break;
            case State.NokiaTask:
                if (!nokiaCollected && nokiaObject == null)
                {
                    nokiaCollected = true;
                    nokiaObjectActivated = true; // Update the flag
                    HideNokiaText();
                    currentState = State.NutellaTask;
                    ShowNutellaText();
                }
                break;
            case State.NutellaTask:
                if (!nutellaCollected && nutellaObject == null)
                {
                    nutellaCollected = true;
                    nutellaObjectActivated = true; // Update the flag
                    HideNutellaText();
                    currentState = State.E_ScooterTask;
                }
                break;
            case State.E_ScooterTask:
                // E-Scooter TextMeshPro will be handled by button press
                break;
        }

        // Additional checks for hiding texts based on object activation
        if (nutellaObject != null && nutellaObject.activeSelf)
        {
            nutellaObjectActivated = true; // Update the flag
            HideNutellaText();
        }

        if (nokiaObject != null && nokiaObject.activeSelf)
        {
            nokiaObjectActivated = true; // Update the flag
            HideNokiaText();
        }

        if (scarfObject != null && scarfObject.activeSelf)
        {
            scarfObjectActivated = true; // Update the flag
            HideScarfText();
        }

        if (costumeObject != null && costumeObject.activeSelf)
        {
            costumeObjectActivated = true; // Update the flag
            HideCostumeText();
        }

        CheckFinishButtonVisibility(); // Check if Finish Button should be visible
    }

    void OnSkipButtonPressed()
    {
        switch (currentState)
        {
            case State.Intro1:
                HideIntroText();
                ShowIntro2Text();
                currentState = State.Intro2;
                break;
            case State.Intro2:
                HideIntro2Text();
                ShowIntro3Text();
                currentState = State.Intro3;
                break;
            case State.Intro3:
                HideIntro3Text();
                ShowServerText();
                ShowServerPicture();
                currentState = State.ServerTask;
                break;
            case State.E_ScooterTask:
                HideEScooterText();
                ShowIntro5Text();
                currentState = State.Finished;
                break;
            case State.Finished:
                if (intro5TextMeshPro.gameObject.activeInHierarchy)
                {
                    HideIntro5Text();
                    ShowNutellaText();
                    ShowNokiaText();
                    ShowScarfText();
                    ShowCostumeText();
                    currentState = State.NutellaTask; // Optionally update state
                }
                break;
        }
        UpdateSkipButtonState();
    }

    void UpdateSkipButtonState()
    {
        if (skipButton != null)
        {
            bool isTextActive = introTextMeshPro.gameObject.activeInHierarchy ||
                                intro2TextMeshPro.gameObject.activeInHierarchy ||
                                intro3TextMeshPro.gameObject.activeInHierarchy ||
                                scanTextMeshPro.gameObject.activeInHierarchy ||
                                intro5TextMeshPro.gameObject.activeInHierarchy;

            skipButton.gameObject.SetActive(isTextActive);
        }
    }

    void HideTaskTexts()
    {
        HideNutellaText();
        HideNokiaText();
        HideScarfText();
        HideCostumeText();
        HideEScooterText();
    }

    void ShowIntroText()
    {
        introTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(introTextMeshPro));
        UpdateSkipButtonState();
    }

    void HideIntroText()
    {
        introTextMeshPro?.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro2Text()
    {
        intro2TextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(intro2TextMeshPro));
        UpdateSkipButtonState();
    }

    void HideIntro2Text()
    {
        intro2TextMeshPro?.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro3Text()
    {
        intro3TextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(intro3TextMeshPro));
        UpdateSkipButtonState();
    }

    void HideIntro3Text()
    {
        intro3TextMeshPro?.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowServerText()
    {
        serverTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(serverTextMeshPro));
        PlaySound();
    }

    void HideServerText()
    {
        serverTextMeshPro?.gameObject.SetActive(false);
    }

    void ShowServerPicture()
    {
        serverPicture?.gameObject.SetActive(true);
    }

    void HideServerPicture()
    {
        serverPicture?.gameObject.SetActive(false);
    }

    void ShowScanText()
    {
        scanTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(scanTextMeshPro));
        PlaySound();
    }

    void HideScanText()
    {
        scanTextMeshPro?.gameObject.SetActive(false);
        // E-Scooter TextMeshPro will be activated by button press
    }

    bool IsScanObjectCompleted()
    {
        return scanObject == null;
    }

    void ShowNokiaText()
    {
        nokiaTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(nokiaTextMeshPro));
        PlaySound();
    }

    void HideNokiaText()
    {
        nokiaTextMeshPro?.gameObject.SetActive(false);
    }

    void ShowNutellaText()
    {
        nutellaTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(nutellaTextMeshPro));
        PlaySound();
        nutellaTextActivated = true; // Track activation
    }

    void HideNutellaText()
    {
        nutellaTextMeshPro?.gameObject.SetActive(false);
        nutellaTextActivated = false; // Reset activation
    }

    void ShowEScooterText()
    {
        eScooterTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(eScooterTextMeshPro));
        PlaySound();
    }

    void HideEScooterText()
    {
        eScooterTextMeshPro?.gameObject.SetActive(false);
    }

    void ShowScarfText()
    {
        scarfTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(scarfTextMeshPro));
        PlaySound();
        scarfTextActivated = true; // Track activation
    }

    void ShowCostumeText()
    {
        costumeTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(costumeTextMeshPro));
        PlaySound();
        costumeTextActivated = true; // Track activation
    }

    void HideScarfText()
    {
        scarfTextMeshPro?.gameObject.SetActive(false);
        scarfTextActivated = false; // Reset activation
    }

    void HideCostumeText()
    {
        costumeTextMeshPro?.gameObject.SetActive(false);
        costumeTextActivated = false; // Reset activation
    }

    void CheckFinishButtonVisibility()
    {
        // Check if Nokia, Nutella, Scarf, and Costume objects have been activated
        if (nokiaObjectActivated && nutellaObjectActivated && scarfObjectActivated && costumeObjectActivated)
        {
            finishButton.gameObject.SetActive(true);
        }
        else
        {
            finishButton.gameObject.SetActive(false);
        }
    }

    void ShowCompletion()
    {
        if (presentTimeCompletePanel != null)
            presentTimeCompletePanel.SetActive(true);
    }

   void OnFinishButtonClicked()
{
    // Deaktiviere das Panel
    if (panel != null)
        panel.SetActive(false);

    // Zeige das Present Time Complete Panel
    if (presentTimeCompletePanel != null)
        presentTimeCompletePanel.SetActive(true);

    // Optionale Überprüfung auf das Finish Button sichtbar
    finishButton.gameObject.SetActive(false);
}


    IEnumerator StartTypingEffect(TextMeshProUGUI textComponent)
    {
        if (audioSource != null && typingSound != null)
            audioSource.PlayOneShot(typingSound);

        string originalText = textComponent.text;
        textComponent.text = "";
        foreach (char c in originalText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(0.05f); // Typing speed
        }
    }

    void PlaySound()
    {
        if (audioSource != null && typingSound != null)
            audioSource.PlayOneShot(typingSound);
    }

    void OnNokiaQuizStartButtonPressed()
    {
        Debug.Log("Nokia quiz started");
    }

    void OnEScooterButtonClicked()
    {
        ShowEScooterText();
        currentState = State.E_ScooterTask;
        UpdateSkipButtonState();
    }

    void OnHideEScooterButtonClicked()
    {
        HideEScooterText();
        ShowIntro5Text();
        currentState = State.Finished;
        UpdateSkipButtonState();
    }

    void ShowIntro5Text()
    {
        intro5TextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(intro5TextMeshPro));
        PlaySound();
        UpdateSkipButtonState();
    }

    void HideIntro5Text()
    {
        intro5TextMeshPro?.gameObject.SetActive(false);
        ShowCostumeAndScarfTexts();
    }

    void ShowCostumeAndScarfTexts()
    {
        costumeTextMeshPro?.gameObject.SetActive(true);
        scarfTextMeshPro?.gameObject.SetActive(true);
        StartCoroutine(StartTypingEffect(costumeTextMeshPro));
        StartCoroutine(StartTypingEffect(scarfTextMeshPro));
        PlaySound();
    }

    void CheckReferences()
    {
        if (serverObject == null) Debug.LogError("Server Object is not assigned!");
        if (scanObject == null) Debug.LogError("Scan Object is not assigned!");
        if (nokiaObject == null) Debug.LogError("Nokia Object is not assigned!");
        if (nutellaObject == null) Debug.LogError("Nutella Object is not assigned!");
        if (scarfObject == null) Debug.LogError("Scarf Object is not assigned!");
        if (costumeObject == null) Debug.LogError("Costume Object is not assigned!");
        if (serverPicture == null) Debug.LogError("Server Picture is not assigned!");
        if (serverTextMeshPro == null) Debug.LogError("Server TextMeshPro is not assigned!");
        if (scanTextMeshPro == null) Debug.LogError("Scan TextMeshPro is not assigned!");
        if (nokiaTextMeshPro == null) Debug.LogError("Nokia TextMeshPro is not assigned!");
        if (introTextMeshPro == null) Debug.LogError("Intro TextMeshPro is not assigned!");
        if (intro2TextMeshPro == null) Debug.LogError("Intro2 TextMeshPro is not assigned!");
        if (intro3TextMeshPro == null) Debug.LogError("Intro3 TextMeshPro is not assigned!");
        if (nutellaTextMeshPro == null) Debug.LogError("Nutella TextMeshPro is not assigned!");
        if (eScooterTextMeshPro == null) Debug.LogError("E-Scooter TextMeshPro is not assigned!");
        if (intro5TextMeshPro == null) Debug.LogError("Intro5 TextMeshPro is not assigned!");
        if (costumeTextMeshPro == null) Debug.LogError("Costume TextMeshPro is not assigned!");
        if (scarfTextMeshPro == null) Debug.LogError("Scarf TextMeshPro is not assigned!");
        if (skipButton == null) Debug.LogError("Skip Button is not assigned!");
        if (nokiaQuizStartButton == null) Debug.LogError("Nokia Quiz Start Button is not assigned!");
        if (eScooterButton == null) Debug.LogError("E-Scooter Button is not assigned!");
        if (hideEScooterButton == null) Debug.LogError("Hide E-Scooter Button is not assigned!");
        if (finishButton == null) Debug.LogError("Finish Button is not assigned!");
        if (audioSource == null) Debug.LogError("Audio Source is not assigned!");
        if (typingSound == null) Debug.LogError("Typing Sound is not assigned!");
        if (canvas == null) Debug.LogError("Canvas is not assigned!");
        if (panel == null) Debug.LogError("Panel is not assigned!");
        if (presentTimeCompletePanel == null) Debug.LogError("Present Time Complete Panel is not assigned!");
        if (taskListPanel == null) Debug.LogError("Task List Panel is not assigned!");
    }
}
