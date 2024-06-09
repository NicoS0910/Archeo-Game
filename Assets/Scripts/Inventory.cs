using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private SerializableDictionary<Resource, int> Resources { get; set;}
    
    //Finds and returns the count of a resource in the dictionary
    public int GetResourceCount(Resource type)
    {
        if(Resources.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        } else {
            return 0;
        }
    }

    public int AddResources(Resource type, int count)
    {
        if(Resources.TryGetValue(type, out int currentCount))
        {
            return Resources[type] += count;
        } else
        {
            Resources.Add(type, count);
            return count;
        }
    }
}
