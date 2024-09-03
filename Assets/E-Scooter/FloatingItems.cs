using System.Collections;
using UnityEngine;

public class FloatingItems : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.5f; // Die Höhe, um die die Objekte schweben sollen
    [SerializeField] private float floatSpeed = 1.0f; // Die Geschwindigkeit des Schwebens

    private Transform[] childTransforms;
    private float[] initialYPositions; // Array, um die ursprünglichen Y-Positionen zu speichern

    private void Start()
    {
        // Holen aller Kind-Objekte
        int childCount = transform.childCount;
        childTransforms = new Transform[childCount];
        initialYPositions = new float[childCount];

        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i); // Alle Kind-Objekte speichern
            initialYPositions[i] = childTransforms[i].localPosition.y; // Die ursprüngliche Y-Position speichern
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
                    // Berechne die neue Y-Position relativ zur ursprünglichen Position
                    float newY = initialYPositions[i] + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                    child.localPosition = new Vector3(child.localPosition.x, newY, child.localPosition.z);
                }
            }

            yield return null; // Warte einen Frame, bevor die Schleife fortgesetzt wird
        }
    }
}
