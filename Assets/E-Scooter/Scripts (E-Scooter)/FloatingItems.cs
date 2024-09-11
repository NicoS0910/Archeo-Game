using System.Collections;
using UnityEngine;

public class FloatingItems : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.5f;
    [SerializeField] private float floatSpeed = 1.0f;

    private Transform[] childTransforms;
    private float[] initialYPositions;

    private void Start()
    {
        int childCount = transform.childCount;
        childTransforms = new Transform[childCount];
        initialYPositions = new float[childCount];

        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
            initialYPositions[i] = childTransforms[i].localPosition.y;
        }

        StartCoroutine(Floating());
    }

    private IEnumerator Floating()
    {
        while (true)
        {
            for (int i = 0; i < childTransforms.Length; i++)
            {
                Transform child = childTransforms[i];

                if (child != null)
                {
                    float newY = initialYPositions[i] + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                    child.localPosition = new Vector3(child.localPosition.x, newY, child.localPosition.z);
                }
            }

            yield return null;
        }
    }
}
