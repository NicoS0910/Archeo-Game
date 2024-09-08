using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button correctAnswerButton;
    public Button[] wrongAnswerButtons;
    public GameObject rewardObject;

    void Start()
    {
        correctAnswerButton.onClick.AddListener(() => OnCorrectButtonClick(correctAnswerButton));
        
        foreach (Button button in wrongAnswerButtons)
        {
            button.onClick.AddListener(() => OnWrongButtonClick(button));
        }

        if (rewardObject != null)
        {
            rewardObject.SetActive(false);
        }
    }

    void OnCorrectButtonClick(Button clickedButton)
    {
        SetButtonColor(clickedButton, Color.green);
        DisableWrongButtons();

        if (rewardObject != null)
        {
            rewardObject.SetActive(true);
        }
    }

    void OnWrongButtonClick(Button clickedButton)
    {
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
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }
}
