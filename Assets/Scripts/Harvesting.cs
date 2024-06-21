using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvesting : MonoBehaviour
{
    public Collider2D swordCollider;
    [field: SerializeField] public Tool Tool { get; private set;}
    public void OnSwordHit(Collider2D collision)
    {
        Harvestable harvestable = collision.GetComponent<Harvestable>();

        if (harvestable != null)
        {
            int amountToHarvest = UnityEngine.Random.Range(Tool.MinHarvest, Tool.MaxHarvest);

            harvestable.TryHarvest(Tool.Type, amountToHarvest);
        }
    }
}
