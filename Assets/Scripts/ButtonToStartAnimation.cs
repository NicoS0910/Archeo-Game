using UnityEngine;
using UnityEngine.UI;

public class ButtonToStartAnimation : MonoBehaviour
{
    public GameObject infoBoxObject;
    private Animator animator;

    void Start()
    {
        if (infoBoxObject != null)
        {
            animator = infoBoxObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on infoBoxObject!");
            }
        }
        else
        {
            Debug.LogError("infoBoxObject is not assigned!");
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ActivateInfoBox);
        }
        else
        {
            Debug.LogError("Button component not found!");
        }
    }

    public void ActivateInfoBox()
    {
        Debug.Log("Button clicked, trying to activate info box.");

        if (animator != null)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("nokia"))
            {
                Debug.Log("Triggering the 'Show' animation.");
                animator.SetTrigger("Show");
            }
            else
            {
                Debug.Log("Animation 'nokia' is already playing.");
            }
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }
}
