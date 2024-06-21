using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvesting : MonoBehaviour
{
    public Collider2D swordCollider;

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

// Update the sprite to show the sprite of the equipped tool
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

    [SerializeField] private Tool _tool;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

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
