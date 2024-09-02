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
    public TextMeshProUGUI intro4TextMeshPro;
    public TextMeshProUGUI intro5TextMeshPro;
    public TextMeshProUGUI intro6TextMeshPro;
    public TextMeshProUGUI intro7TextMeshPro;
    public TextMeshProUGUI nutellaTextMeshPro;
    public TextMeshProUGUI eScooterTextMeshPro;
    public Button skipButton;
    public Button nokiaQuizStartButton;
    public AudioSource audioSource;
    public AudioClip typingSound;

    public Canvas canvas; // Reference to the Canvas
    public GameObject panel; // Reference to the Panel
    public GameObject presentTimeCompletePanel; // Reference to the PresentTimeComplete Panel
    public GameObject taskListPanel; // Reference to the Task List Panel

    private enum State
    {
        Intro1,
        Intro2,
        ServerTask,
        Intro3,
        ScanTask,
        Intro4,
        NokiaTask,
        Intro5,
        NutellaTask,
        Intro6,
        E_ScooterTask,
        Intro7,
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

        // Deactivate Canvas, Panel, and PresentTimeCompletePanel initially
        if (canvas != null)
            canvas.gameObject.SetActive(false);

        if (panel != null)
            panel.SetActive(false);

        if (presentTimeCompletePanel != null)
            presentTimeCompletePanel.SetActive(false);

        // Start coroutine with delay using Invoke
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
        HideIntro4Text();
        HideIntro5Text();
        HideIntro6Text();
        HideIntro7Text();
        HideTaskTexts();

        skipButton.onClick.AddListener(OnSkipButtonPressed);
        nokiaQuizStartButton.onClick.AddListener(OnNokiaQuizStartButtonPressed);
        UpdateSkipButtonState();
        yield return null; // Just to ensure the coroutine completes in this frame
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
                if (!nokiaCollected && (nokiaObject == null || !nokiaObject.activeInHierarchy))
                {
                    nokiaCollected = true;
                    HideNokiaText();
                    currentState = State.Intro5;
                    ShowIntro5Text();
                }
                break;
            case State.NutellaTask:
                if (!nutellaCollected && (nutellaObject == null || !nutellaObject.activeInHierarchy))
                {
                    nutellaCollected = true;
                    HideNutellaText();
                    currentState = State.Intro6;
                    ShowIntro6Text();
                }
                break;
            case State.E_ScooterTask:
                if (!eScooterCollected && (eScooterObject == null || !eScooterObject.activeInHierarchy))
                {
                    eScooterCollected = true;
                    HideE_ScooterText();
                    currentState = State.Intro7;
                    ShowIntro7Text();
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
        if (nutellaObject == null)
            Debug.LogError("Nutella object reference is not assigned in the inspector.");
        if (eScooterObject == null)
            Debug.LogError("E-Scooter object reference is not assigned in the inspector.");
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
        if (intro5TextMeshPro == null)
            Debug.LogError("Intro5 TextMeshProUGUI component is not assigned in the inspector.");
        if (intro6TextMeshPro == null)
            Debug.LogError("Intro6 TextMeshProUGUI component is not assigned in the inspector.");
        if (intro7TextMeshPro == null)
            Debug.LogError("Intro7 TextMeshProUGUI component is not assigned in the inspector.");
        if (nutellaTextMeshPro == null)
            Debug.LogError("Nutella TextMeshProUGUI component is not assigned in the inspector.");
        if (eScooterTextMeshPro == null)
            Debug.LogError("E-Scooter TextMeshProUGUI component is not assigned in the inspector.");
        if (skipButton == null)
            Debug.LogError("Skip Button reference is not assigned in the inspector.");
        if (nokiaQuizStartButton == null)
            Debug.LogError("Nokia Quiz Start Button reference is not assigned in the inspector.");
        if (audioSource == null)
            Debug.LogError("AudioSource reference is not assigned in the inspector.");
        if (typingSound == null)
            Debug.LogError("Typing sound reference is not assigned in the inspector.");
        if (canvas == null)
            Debug.LogError("Canvas reference is not assigned in the inspector.");
        if (panel == null)
            Debug.LogError("Panel reference is not assigned in the inspector.");
        if (presentTimeCompletePanel == null)
            Debug.LogError("PresentTimeCompletePanel reference is not assigned in the inspector.");
        if (taskListPanel == null)
            Debug.LogError("TaskListPanel reference is not assigned in the inspector.");
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

    void ShowIntro5Text()
    {
        if (intro5TextMeshPro != null)
        {
            intro5TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro5TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro5Text()
    {
        if (intro5TextMeshPro != null)
            intro5TextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro6Text()
    {
        if (intro6TextMeshPro != null)
        {
            intro6TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro6TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro6Text()
    {
        if (intro6TextMeshPro != null)
            intro6TextMeshPro.gameObject.SetActive(false);
        UpdateSkipButtonState();
    }

    void ShowIntro7Text()
    {
        if (intro7TextMeshPro != null)
        {
            intro7TextMeshPro.gameObject.SetActive(true);
            StartCoroutine(StartTypingEffect(intro7TextMeshPro));
        }
        UpdateSkipButtonState();
    }

    void HideIntro7Text()
    {
        if (intro7TextMeshPro != null)
            intro7TextMeshPro.gameObject.SetActive(false);
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
    if (serverPicture != null)
    {
        serverPicture.gameObject.SetActive(true); // Zeige das RawImage an
    }
}


void HideServerText()
{
    if (serverTextMeshPro != null)
        serverTextMeshPro.gameObject.SetActive(false);
    if (serverPicture != null)
        serverPicture.gameObject.SetActive(false); // Verstecke das RawImage
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
        HideServerText();
        HideScanText();
        HideNokiaText();
        HideNutellaText();
        HideE_ScooterText();
    }

    void UpdateSkipButtonState()
    {
        if (currentState == State.Intro1 || currentState == State.Intro2 || 
            currentState == State.Intro3 || currentState == State.Intro4 || 
            currentState == State.Intro5 || currentState == State.Intro6 || 
            currentState == State.Intro7)
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
                currentState = State.Intro5;
                HideNokiaText();
                ShowIntro5Text();
                break;
            case State.Intro5:
                currentState = State.NutellaTask;
                HideIntro5Text();
                ShowNutellaText();
                break;
            case State.NutellaTask:
                currentState = State.Intro6;
                HideNutellaText();
                ShowIntro6Text();
                break;
            case State.Intro6:
                currentState = State.E_ScooterTask;
                HideIntro6Text();
                ShowE_ScooterText();
                break;
            case State.E_ScooterTask:
                currentState = State.Intro7;
                HideE_ScooterText();
                ShowIntro7Text();
                break;
            case State.Intro7:
                HideIntro7Text();
                if (taskListPanel != null)
                    taskListPanel.SetActive(false);
                if (presentTimeCompletePanel != null)
                    presentTimeCompletePanel.SetActive(true);
                currentState = State.Finished;
                break;
        }

        UpdateSkipButtonState();
    }

    public void OnNokiaQuizStartButtonPressed()
    {
        // Ensure this button only affects the Nokia task
        if (currentState == State.NokiaTask)
        {
            nokiaCollected = true;
            HideNokiaText();
            currentState = State.Intro5;
            ShowIntro5Text();
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
            if (renderer != null && renderer.sprite != null && renderer.sprite.name == "hennes_64")
            {
                return true;
            }
        }
        return false;
    }

    void PlaySound()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }
}
