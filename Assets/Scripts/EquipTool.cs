using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : MonoBehaviour
{
    [field: SerializeField] public Tool Tool { get; set;}
    private Harvesting _targetHarvesting;

    private void Start()
    {
        _targetHarvesting = FindObjectOfType<PlayerController>().GetComponentInChildren<Harvesting>();
    }

    public void ChangeTool()
    {
        if(_targetHarvesting != null)
        {
            _targetHarvesting.Tool = Tool;
        }
        else{
            Debug.LogWarning("The target harvesting component is no longer referenced. Is the player still active in scene?");
        }
    }
}
