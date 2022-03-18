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
        this.durability = item.maxDurability;
        this.maxDurability = item.maxDurability;
        onDurabilityChanged(durability, maxDurability);
    }

    public DurabilityItem(float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(description, weight, maxStack, icon, sprite, inventory, amount)
    {
        this.durability = maxDurability;
        this.maxDurability = maxDurability;
        onDurabilityChanged(durability, maxDurability);
    }

    public override Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new DurabilityItem((DurabilityItem)item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    public float Durability 
    { 
        get => durability; 
        set
        {
            durability = Mathf.Clamp(value, 0, maxDurability);
            if(durability <= 0) 
            {
                Amount -= 1;
                if (Amount <= 0) inventory.RemoveItem(this);
                else durability = maxDurability;
            }
            onDurabilityChanged(durability, maxDurability);
        } 
    }
}
