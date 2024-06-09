using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] public int MinHarvest { get; private set; } = 1;
    [SerializeField] public int MaxHarvest { get; private set; } = 3;
    public Collider2D swordCollider;
    public void OnSwordHit(Collider2D collision)
    {
        Harvestable harvestable = collision.GetComponent<Harvestable>();

        if (harvestable != null)
        {
            int amountToHarvest = UnityEngine.Random.Range(MinHarvest, MaxHarvest);
            harvestable.Harvest(amountToHarvest);
        }
    }
}
