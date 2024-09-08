using UnityEngine;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    public GameObject targetObject;
    public string playerTag = "Player";

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("TargetObject is not assigned!");
        }
        else
        {
            targetObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log("TargetObject activated!");
        }
    }
}
