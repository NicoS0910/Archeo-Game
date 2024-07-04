using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button correctAnswerButton;   // Der Button, der die korrekte Antwort darstellt
    public Button[] wrongAnswerButtons;  // Die Buttons, die falsche Antworten darstellen

    void Start()
    {
        // Fügen Sie jedem Button einen Listener hinzu, um auf Klicks zu reagieren
        correctAnswerButton.onClick.AddListener(() => OnCorrectButtonClick(correctAnswerButton));
        
        foreach (Button button in wrongAnswerButtons)
        {
            button.onClick.AddListener(() => OnWrongButtonClick(button));
        }
    }

    void OnCorrectButtonClick(Button clickedButton)
    {
        // Markiere den korrekten Button grün
        SetButtonColor(clickedButton, Color.green);

        // Deaktiviere alle falschen Buttons
        DisableWrongButtons();
    }

    void OnWrongButtonClick(Button clickedButton)
    {
        // Markiere den falschen Button rot
        SetButtonColor(clickedButton, Color.red);
    }

    void DisableWrongButtons()
    {
        foreach (Button button in wrongAnswerButtons)
        {
            button.interactable = false;
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        // Ändern Sie die Farbe der Image-Komponente des Buttons
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }
}
