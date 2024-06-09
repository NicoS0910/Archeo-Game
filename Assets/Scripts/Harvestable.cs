using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [field: SerializeField] public int ResourceCount { get; private set;}
    [field: SerializeField] public ParticleSystem ResourceEmitPS { get; private set;}
    private int _amountHarvested = 0;

    public void Harvest(int amount)
    {
        //cant harvest more resources than are left in the node
        int amountToSpawn = Mathf.Min(amount, ResourceCount - _amountHarvested);

        if(amountToSpawn > 0)
        {
            ResourceEmitPS.Emit(amountToSpawn);
            _amountHarvested += amountToSpawn;
        }

        if(_amountHarvested >= ResourceCount)
        {
            //Node os depleted
            Destroy(gameObject);
        }
    }
}
