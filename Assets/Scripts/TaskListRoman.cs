using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListRoman : MonoBehaviour
{
    // Referenzen zu Target Objects
    public GameObject augustusObject;
    public GameObject glassObject;
    public GameObject ringObject;
    public GameObject mosaikObject;
    public GameObject oilAmphoraObject;
    public GameObject venusObject;
    public GameObject fortunaObject;

    // Referenzen zu TextMeshProUGUI für jedes Target Object
    public TextMeshProUGUI augustusTextMeshPro;
    public TextMeshProUGUI glassTextMeshPro;
    public TextMeshProUGUI ringTextMeshPro;
    public TextMeshProUGUI mosaikTextMeshPro;
    public TextMeshProUGUI oilAmphoraTextMeshPro;
    public TextMeshProUGUI venusTextMeshPro;
    public TextMeshProUGUI fortunaTextMeshPro;

    // Neue Titel-Textkomponente
    public TextMeshProUGUI titelTextMeshPro;

    // Referenzen zu Buttons und Panels
    public Button finishButton;
    public GameObject romanTimeCompletePanel;
    public GameObject romanTaskListPanel; // Das Roman Task List Panel

    // Start is called before the first frame update
    void Start()
    {
        // Initialisiere die UI-Elemente
        InitializeUI();

        // Finish-Button verstecken
        if (finishButton != null)
            finishButton.gameObject.SetActive(false);

        // Event dem Finish-Button zuweisen
        if (finishButton != null)
            finishButton.onClick.AddListener(OnFinishButtonPressed);
    }

    // Initialisiere die UI-Elemente
    void InitializeUI()
    {
        // Alle TextMeshProUGUI-Elemente initial anzeigen
        if (augustusTextMeshPro != null)
            augustusTextMeshPro.gameObject.SetActive(true);
        if (glassTextMeshPro != null)
            glassTextMeshPro.gameObject.SetActive(true);
        if (ringTextMeshPro != null)
            ringTextMeshPro.gameObject.SetActive(true);
        if (mosaikTextMeshPro != null)
            mosaikTextMeshPro.gameObject.SetActive(true);
        if (oilAmphoraTextMeshPro != null)
            oilAmphoraTextMeshPro.gameObject.SetActive(true);
        if (venusTextMeshPro != null)
            venusTextMeshPro.gameObject.SetActive(true);
        if (fortunaTextMeshPro != null)
            fortunaTextMeshPro.gameObject.SetActive(true);

        // Titel-Text anzeigen
        if (titelTextMeshPro != null)
            titelTextMeshPro.gameObject.SetActive(true);

        // Roman Time Complete Panel und Roman Task List Panel initial verstecken
        if (romanTimeCompletePanel != null)
            romanTimeCompletePanel.SetActive(false);

        if (romanTaskListPanel != null)
            romanTaskListPanel.SetActive(true); // Das Roman Task List Panel wird zu Beginn angezeigt
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfe den Status jedes Target Objects und verstecke das jeweilige TextMeshPro, wenn das Object aktiv ist
        if (augustusObject != null && augustusObject.activeInHierarchy && augustusTextMeshPro != null)
            augustusTextMeshPro.gameObject.SetActive(false);

        if (glassObject != null && glassObject.activeInHierarchy && glassTextMeshPro != null)
            glassTextMeshPro.gameObject.SetActive(false);

        if (ringObject != null && ringObject.activeInHierarchy && ringTextMeshPro != null)
            ringTextMeshPro.gameObject.SetActive(false);

        if (mosaikObject != null && mosaikObject.activeInHierarchy && mosaikTextMeshPro != null)
            mosaikTextMeshPro.gameObject.SetActive(false);

        if (oilAmphoraObject != null && oilAmphoraObject.activeInHierarchy && oilAmphoraTextMeshPro != null)
            oilAmphoraTextMeshPro.gameObject.SetActive(false);

        if (venusObject != null && venusObject.activeInHierarchy && venusTextMeshPro != null)
            venusTextMeshPro.gameObject.SetActive(false);

        if (fortunaObject != null && fortunaObject.activeInHierarchy && fortunaTextMeshPro != null)
            fortunaTextMeshPro.gameObject.SetActive(false);

        // Prüfe, ob alle TextMeshPro deaktiviert sind, dann Finish-Button anzeigen
        if (AllTextMeshProDeactivated() && finishButton != null)
        {
            finishButton.gameObject.SetActive(true);
        }
    }

    // Funktion um zu überprüfen, ob alle TextMeshPros deaktiviert wurden
    bool AllTextMeshProDeactivated()
    {
        return !augustusTextMeshPro.gameObject.activeInHierarchy &&
               !glassTextMeshPro.gameObject.activeInHierarchy &&
               !ringTextMeshPro.gameObject.activeInHierarchy &&
               !mosaikTextMeshPro.gameObject.activeInHierarchy &&
               !oilAmphoraTextMeshPro.gameObject.activeInHierarchy &&
               !venusTextMeshPro.gameObject.activeInHierarchy &&
               !fortunaTextMeshPro.gameObject.activeInHierarchy;
    }

    // Handler für den Finish-Button
    void OnFinishButtonPressed()
    {
        // Deaktiviere den Finish-Button und zeige das Roman Time Complete Panel an
        if (finishButton != null)
            finishButton.gameObject.SetActive(false);

        if (romanTimeCompletePanel != null)
            romanTimeCompletePanel.SetActive(true);

        // Roman Task List Panel deaktivieren
        if (romanTaskListPanel != null)
            romanTaskListPanel.SetActive(false);

        // Titel-Text deaktivieren
        if (titelTextMeshPro != null)
            titelTextMeshPro.gameObject.SetActive(false);
    }
}
