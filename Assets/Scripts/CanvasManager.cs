using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject serverObject;
    public GameObject scanObject;
    public GameObject nokiaObject;
    public GameObject nutellaObject;
    public GameObject eScooterObject;
    public RawImage serverPicture;
    public TextMeshProUGUI serverTextMeshPro;
    public TextMeshProUGUI scanTextMeshPro;
    public TextMeshProUGUI nokiaTextMeshPro;
    public TextMeshProUGUI introTextMeshPro;
    public TextMeshProUGUI intro2TextMeshPro;
    public TextMeshProUGUI intro3TextMeshPro;
    public TextMeshProUGUI nutellaTextMeshPro;
    public TextMeshProUGUI eScooterTextMeshPro;
    public Button skipButton;
    public Button nokiaQuizStartButton;
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
        ServerTask,
        Intro3,
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
    private bool eScooterCollected = false;

    void Start()
    {
        CheckReferences();

        if (canvas != null)
            canvas.gameObject.SetActive(false);

        if (panel != null)
            panel.SetActive(false);

        if (presentTimeCompletePanel != null)
            presentTimeCompletePanel.SetActive(false);

        // Set initial visibility
        if (serverPicture != null)
            serverPicture.gameObject.SetActive(false);
        if (serverTextMeshPro != null)
            serverTextMeshPro.gameObject.SetActive(false);
        if (scanTextMeshPro != null)
            scanTextMeshPro.gameObject.SetActive(false);

        Invoke("ActivateCanvasAndPanel", 3f);
    }

    void ActivateCanvasAndPanel()
    {
        if (canvas != null)
            canvas.gameObject.SetActive(true);

        if (panel != null)
            panel.SetActive(true);

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
                    HideServerPicture(); // Hide server picture when server is collected
                    currentState = State.Intro3;
                    ShowIntro3Text();
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
                break;
            case State.NokiaTask:
                if (!nokiaCollected && nokiaObject == null)
                {
                    nokiaCollected = true;
                    HideNokiaText();
                    currentState = State.NutellaTask;
                    ShowNutellaText();
                }
                break;
            case State.NutellaTask:
                if (!nutellaCollected && nutellaObject == null)
                {
                    nutellaCollected = true;
                    HideNutellaText();
                    currentState = State.E_ScooterTask;
                    ShowE_ScooterText();
                }
                break;
            case State.E_ScooterTask:
                if (!eScooterCollected && eScooterObject == null)
                {
                    eScooterCollected = true;
                    HideE_ScooterText();
                    currentState = State.Finished;
                    ShowCompletion();
                }
                break;
        }
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
                currentState = State.ServerTask;
                ShowServerText();
                ShowServerPicture(); // Show server picture with the server text
                break;
            case State.Intro3:
                HideIntro3Text();
                currentState = State.ScanTask;
                ShowScanText(); // Show scan text after Intro 3
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
                                scanTextMeshPro.gameObject.activeInHierarchy;

            skipButton.gameObject.SetActive(isTextActive);
        }
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

    void ShowServerText()
    {
        if (serverTextMeshPro != null)
        {
            serverTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(serverTextMeshPro));
            PlaySound();
        }
    }

    void HideServerText()
    {
        if (serverTextMeshPro != null)
            serverTextMeshPro.gameObject.SetActive(false);
    }

    void ShowServerPicture()
    {
        if (serverPicture != null)
        {
            serverPicture.gameObject.SetActive(true); // Show the RawImage
        }
    }

    void HideServerPicture()
    {
        if (serverPicture != null)
            serverPicture.gameObject.SetActive(false); // Hide the RawImage
    }

    void ShowScanText()
    {
        if (scanTextMeshPro != null)
        {
            scanTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(scanTextMeshPro));
            PlaySound();
        }
    }

    void HideScanText()
    {
        if (scanTextMeshPro != null)
            scanTextMeshPro.gameObject.SetActive(false);
    }

    bool IsScanObjectCompleted()
    {
        return scanObject == null; // Example check: Object is destroyed or collected
    }

    void ShowNokiaText()
    {
        if (nokiaTextMeshPro != null)
        {
            nokiaTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(nokiaTextMeshPro));
            PlaySound();
        }
    }

    void HideNokiaText()
    {
        if (nokiaTextMeshPro != null)
            nokiaTextMeshPro.gameObject.SetActive(false);
    }

    void ShowNutellaText()
    {
        if (nutellaTextMeshPro != null)
        {
            nutellaTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(nutellaTextMeshPro));
            PlaySound();
        }
    }

    void HideNutellaText()
    {
        if (nutellaTextMeshPro != null)
            nutellaTextMeshPro.gameObject.SetActive(false);
    }

    void ShowE_ScooterText()
    {
        if (eScooterTextMeshPro != null)
        {
            eScooterTextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(eScooterTextMeshPro));
            PlaySound();
        }
    }

    void HideE_ScooterText()
    {
        if (eScooterTextMeshPro != null)
            eScooterTextMeshPro.gameObject.SetActive(false);
    }

    void HideTaskTexts()
    {
        HideScanText();
        HideNokiaText();
        HideNutellaText();
        HideE_ScooterText();
    }

    void ShowCompletion()
    {
        if (presentTimeCompletePanel != null)
            presentTimeCompletePanel.SetActive(true);
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

    void OnNokiaQuizStartButtonPressed()
    {
        // Logic for starting Nokia quiz
    }

    void CheckReferences()
    {
        if (serverTextMeshPro == null || scanTextMeshPro == null || nokiaTextMeshPro == null ||
            introTextMeshPro == null || intro2TextMeshPro == null || intro3TextMeshPro == null ||
            nutellaTextMeshPro == null || eScooterTextMeshPro == null || skipButton == null ||
            nokiaQuizStartButton == null || audioSource == null || typingSound == null ||
            canvas == null || panel == null || presentTimeCompletePanel == null || taskListPanel == null ||
            serverPicture == null)
        {
            Debug.LogError("One or more references are not assigned in the inspector.");
        }
    }

    void PlaySound()
    {
        if (audioSource != null && typingSound != null)
            audioSource.PlayOneShot(typingSound);
    }
}
