using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [field: SerializeField] private SerializableDictionary<Resource, int> Resources { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Inventar bleibt über Szenen hinweg erhalten
        }
        else
        {
            Destroy(gameObject); // Verhindert doppelte Instanzen
        }
    }

    //Finds and returns the count of a resource in the dictionary
    public int GetResourceCount(Resource type)
    {
        if (Resources.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        }
        else
        {
            return 0;
        }
    }

    public int AddResources(Resource type, int count)
    {
        if (Resources.TryGetValue(type, out int currentCount))
        {
            return Resources[type] += count;
        }
        else
        {
            Resources.Add(type, count);
            return count;
        }
    }
}
