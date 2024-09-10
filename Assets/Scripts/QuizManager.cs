using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button correctAnswerButton;
    public Button[] wrongAnswerButtons;
    public GameObject rewardObject;
    public Inventory inventory;
    public Resource scorePoints;
    private bool isClicked = false;

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
        if (isClicked == false)
        {
            SetButtonColor(clickedButton, Color.green);
            DisableWrongButtons();
            isClicked = true;
            inventory.AddResources(scorePoints, 100);
            Debug.Log("Correct Button clicked");

            if (rewardObject != null)
            {
                rewardObject.SetActive(true);
            }
        } else {
            Debug.Log("Correct Button already clicked");
        }
    }

    void OnWrongButtonClick(Button clickedButton)
    {
        SetButtonColor(clickedButton, Color.red);
        inventory.AddResources(scorePoints, -30);
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
