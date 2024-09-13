using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListMedieval : MonoBehaviour
{
    // Referenzen zu Target Objects
    public GameObject swordTargetObject;
    public GameObject monstranzTargetObject;
    public GameObject ursulaTargetObject;
    public GameObject akolythTargetObject;
    public GameObject trauerdalmatikTargetObject;
    public GameObject gewandaspangeTargetObject;
    public GameObject coinsTargetObject; // Neues Coin-Object

    // Referenzen zu TextMeshProUGUI für jedes Target Object
    public TextMeshProUGUI swordTextMeshPro;
    public TextMeshProUGUI monstranzTextMeshPro;
    public TextMeshProUGUI ursulaTextMeshPro;
    public TextMeshProUGUI akolythTextMeshPro;
    public TextMeshProUGUI trauerdalmatikTextMeshPro;
    public TextMeshProUGUI gewandaspangeTextMeshPro;
    public TextMeshProUGUI coinsTextMeshPro; // Neues Coins TextMeshPro

    // Neue Titel-Textkomponente
    public TextMeshProUGUI titelTextMeshPro;

    // Referenzen zu Buttons und Panels
    public Button finishButton;
    public GameObject medievalTimeCompletePanel;
    public GameObject medievalTaskListPanel; // Das Medieval Task List Panel

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
        if (swordTextMeshPro != null)
            swordTextMeshPro.gameObject.SetActive(true);
        if (monstranzTextMeshPro != null)
            monstranzTextMeshPro.gameObject.SetActive(true);
        if (ursulaTextMeshPro != null)
            ursulaTextMeshPro.gameObject.SetActive(true);
        if (akolythTextMeshPro != null)
            akolythTextMeshPro.gameObject.SetActive(true);
        if (trauerdalmatikTextMeshPro != null)
            trauerdalmatikTextMeshPro.gameObject.SetActive(true);
        if (gewandaspangeTextMeshPro != null)
            gewandaspangeTextMeshPro.gameObject.SetActive(true);
        if (coinsTextMeshPro != null) // Coins TextMeshPro initial anzeigen
            coinsTextMeshPro.gameObject.SetActive(true);

        // Titel-Text anzeigen
        if (titelTextMeshPro != null)
            titelTextMeshPro.gameObject.SetActive(true);

        // Panels initial verstecken
        if (medievalTimeCompletePanel != null)
            medievalTimeCompletePanel.SetActive(false);

        if (medievalTaskListPanel != null)
            medievalTaskListPanel.SetActive(true); // Das Medieval Task List Panel wird zu Beginn angezeigt
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfe den Status jedes Target Objects und verstecke das jeweilige TextMeshPro, wenn das Object aktiv ist
        if (swordTargetObject != null && swordTargetObject.activeInHierarchy && swordTextMeshPro != null)
            swordTextMeshPro.gameObject.SetActive(false);

        if (monstranzTargetObject != null && monstranzTargetObject.activeInHierarchy && monstranzTextMeshPro != null)
            monstranzTextMeshPro.gameObject.SetActive(false);

        if (ursulaTargetObject != null && ursulaTargetObject.activeInHierarchy && ursulaTextMeshPro != null)
            ursulaTextMeshPro.gameObject.SetActive(false);

        if (akolythTargetObject != null && akolythTargetObject.activeInHierarchy && akolythTextMeshPro != null)
            akolythTextMeshPro.gameObject.SetActive(false);

        if (trauerdalmatikTargetObject != null && trauerdalmatikTargetObject.activeInHierarchy && trauerdalmatikTextMeshPro != null)
            trauerdalmatikTextMeshPro.gameObject.SetActive(false);

        if (gewandaspangeTargetObject != null && gewandaspangeTargetObject.activeInHierarchy && gewandaspangeTextMeshPro != null)
            gewandaspangeTextMeshPro.gameObject.SetActive(false);

        if (coinsTargetObject != null && coinsTargetObject.activeInHierarchy && coinsTextMeshPro != null) // Coins Logik
            coinsTextMeshPro.gameObject.SetActive(false);

        // Prüfe, ob alle TextMeshPro deaktiviert sind, dann Finish-Button anzeigen
        if (AllTextMeshProDeactivated() && finishButton != null)
        {
            finishButton.gameObject.SetActive(true);
        }
    }

    // Funktion um zu überprüfen, ob alle TextMeshPros deaktiviert wurden
    bool AllTextMeshProDeactivated()
    {
        return !swordTextMeshPro.gameObject.activeInHierarchy &&
               !monstranzTextMeshPro.gameObject.activeInHierarchy &&
               !ursulaTextMeshPro.gameObject.activeInHierarchy &&
               !akolythTextMeshPro.gameObject.activeInHierarchy &&
               !trauerdalmatikTextMeshPro.gameObject.activeInHierarchy &&
               !gewandaspangeTextMeshPro.gameObject.activeInHierarchy &&
               !coinsTextMeshPro.gameObject.activeInHierarchy; // Coins TextMeshPro prüfen
    }

    // Handler für den Finish-Button
    void OnFinishButtonPressed()
    {
        // Deaktiviere das aktuelle Panel
        if (medievalTaskListPanel != null)
            medievalTaskListPanel.SetActive(false);

        // Zeige das Medieval Time Complete Panel an
        if (medievalTimeCompletePanel != null)
            medievalTimeCompletePanel.SetActive(true);

        // Titel-Text deaktivieren
        if (titelTextMeshPro != null)
            titelTextMeshPro.gameObject.SetActive(false);
    }
}
