using UnityEngine;

public class DestroyObjectWhenOtherObjectDestroyed : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject objectToDestroy;

    private bool targetObjectDestroyed = false;

    void Update()
    {
        if (targetObject == null && !targetObjectDestroyed)
        {
            targetObjectDestroyed = true;
            Destroy(objectToDestroy);
        }
    }
}
