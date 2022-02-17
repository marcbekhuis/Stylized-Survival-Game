using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New useable item", menuName = "Items/UseableItem")]
public class UseableItem : DurabilityItem
{
    public delegate void OnItemUse(UseableItem item);
    public event OnItemUse onItemSelected = delegate { };
    public event OnItemUse onItemDeselected = delegate { };
    public event OnItemUse onItemUsePrimairy = delegate { };
    public event OnItemUse onItemUseSecundairy = delegate { };

    public UseableItem(UseableItem item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
    }

    public UseableItem(float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
    }

    public virtual void Select(Transform userTransform, Transform itemTransform)
    {
        onItemSelected(this);
    }

    public virtual void Deselect(Transform userTransform, Transform itemTransform)
    {
        onItemDeselected(this);
    }

    public virtual void UsePrimairy(Transform userTransform, Transform itemTransform)
    {
        onItemUsePrimairy(this);
    }

    public virtual void UseSecondairy(Transform userTransform, Transform itemTransform)
    {
        onItemUseSecundairy(this);
    }
}
