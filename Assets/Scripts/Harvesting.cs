using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvesting : MonoBehaviour
{
    public Collider2D swordCollider;
    public Animator playerAnimator; // Referenz zum Animator des Spielers
    private SpriteRenderer _renderer;
    public PlayerController playerController;

    [SerializeField] private Tool _tool;
    [SerializeField] private ToolType scannerToolType; // Referenz zum Scanner-ToolType
    [SerializeField] private ToolType pickaxeToolType; // Referenz zum Pickaxe-ToolType
    [SerializeField] private GameObject scanAnimationObject; // Das Scan-Animations-Objekt

    public Tool Tool
    {
        get
        {
            return _tool;
        }
        set
        {
            if (_tool != value)
            {
                _tool = value;
                UpdateSprite();
            }
        }
    }

    private void UpdateSprite()
    {
        if (_tool != null)
        {
            _renderer.sprite = _tool.Sprite;
        }
        else
        {
            _renderer.sprite = null;
        }
    }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
        DeactivateScanAnimation(); // Stelle sicher, dass die Animation zu Beginn deaktiviert ist
    }

    public void OnSwordHit(Collider2D collision)
    {
        // Überprüft, ob das getroffene Objekt ein "Harvestable"-Objekt ist
        Harvestable harvestable = collision.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            int amountToHarvest = UnityEngine.Random.Range(Tool.MinHarvest, Tool.MaxHarvest);
            harvestable.TryHarvest(Tool.Type, amountToHarvest);
        }

        // Überprüft, ob das getroffene Objekt ein "ScanObject" ist
        ScanObject scanObject = collision.GetComponent<ScanObject>();
        if (scanObject != null)
        {
            scanObject.TryActivate(Tool.Type);
        }
    }

    public void OnScanHit(Collider2D collision)
    {
        // Überprüft, ob das getroffene Objekt ein "ScanArtefact"-Objekt ist
        ScanArtefact scanArtefact = collision.GetComponent<ScanArtefact>();
        if (scanArtefact != null)
        {
            // Überprüft, ob das aktuelle Tool vom Typ Scanner ist
            if (Tool.Type == scannerToolType)
            {
                // Spielt die Scan-Animation ab
                playerAnimator.SetTrigger("scan");

                // Versucht, das Artefakt zu aktivieren
                scanArtefact.TryActivate(Tool.Type);
            }
            else
            {
                playerController.EndScan();
            }
        }
        else
        {
            playerController.EndScan();
        }
    }

    // Funktion zum Aktivieren des Scan-Animations-Objekts
    public void ActivateScanAnimation()
    {
        if (scanAnimationObject != null)
        {
            scanAnimationObject.SetActive(true);
        }
    }

    // Funktion zum Deaktivieren des Scan-Animations-Objekts
    public void DeactivateScanAnimation()
    {
        if (scanAnimationObject != null)
        {
            scanAnimationObject.SetActive(false);
        }
    }
}
