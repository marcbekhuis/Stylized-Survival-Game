using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New durability item", menuName = "Items/DurabilityItem")]
public class DurabilityItem : Item
{
    [Header("Durability")]
    public float maxDurability = 1;

    private float durability;

    public delegate void OnDurabilityChange(float durability, float maxDurability);
    public event OnDurabilityChange onDurabilityChanged = delegate { };

    public DurabilityItem(DurabilityItem item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
        this.durability = item.durability;
        this.maxDurability = item.maxDurability;
    }

    public DurabilityItem(float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(description, weight, maxStack, icon, sprite, inventory, amount)
    {
        this.durability = durability;
        this.maxDurability = maxDurability;
    }

    public float Durability 
    { 
        get => durability; 
        set
        {
            durability = Mathf.Clamp(durability + value, 0, maxDurability);
            onDurabilityChanged(durability, maxDurability);
        } 
    }
}
