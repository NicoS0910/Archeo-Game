using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Harvesting harvestingScript;

    private void Start()
    {
        if (harvestingScript == null)
        {
            // Falls das Script nicht direkt verlinkt wurde, suchen wir es im Kindobjekt
            harvestingScript = GetComponentInChildren<Harvesting>();
        }
    }

    public void ActivateScanAnimation()
    {
        if (harvestingScript != null)
        {
            harvestingScript.ActivateScanAnimation();
        }
    }

    public void DeactivateScanAnimation()
    {
        if (harvestingScript != null)
        {
            harvestingScript.DeactivateScanAnimation();
        }
    }
}
