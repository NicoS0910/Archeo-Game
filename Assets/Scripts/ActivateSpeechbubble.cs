using UnityEngine;

public class ActivateSpeechbubble : MonoBehaviour
{
    public float interactionRange = 5f;
    public GameObject objectToActivate;

    private bool isInRange = false;

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= interactionRange)
            {
                isInRange = true;
                ActivateObject();
            }
            else
            {
                isInRange = false;
                DeactivateObject();
            }
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null && !objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(true);
        }
    }

    void DeactivateObject()
    {
        if (objectToActivate != null && objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
