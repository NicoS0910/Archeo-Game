using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [field: SerializeField] public ToolType HarvestingType { get; private set; }
    [field: SerializeField] public int ResourceCount { get; private set; }
    [field: SerializeField] public ParticleSystem ResourceEmitPS { get; private set; }
    [SerializeField] private AudioClip harvestSoundClip; //hinterlegter Sound
    private int _amountHarvested = 0;

//Checks tool type to make sure harvesting the node is possible
    public bool TryHarvest(ToolType harvestingType, int amount)
    {
        if (harvestingType == HarvestingType)
        {
            Harvest(amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Harvest(int amount)
    {
        //cant harvest more resources than are left in the node
        int amountToSpawn = Mathf.Min(amount, ResourceCount - _amountHarvested);

        if (amountToSpawn > 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(harvestSoundClip, transform, 1f);
            if (ResourceEmitPS != null)
            {
                ResourceEmitPS.Emit(amountToSpawn);
            }
            _amountHarvested += amountToSpawn;
        }

        if (_amountHarvested >= ResourceCount)
        {
            //Node is depleted
            SoundFXManager.instance.PlaySoundFXClip(harvestSoundClip, transform, 1f);
            Destroy(gameObject);
        }
    }
}
