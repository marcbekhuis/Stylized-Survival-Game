using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItem : DurabilityItem
{
    public delegate void OnItemUse(UseableItem item);
    public static event OnItemUse onItemSelected = delegate { };
    public static event OnItemUse onItemDeselected = delegate { };
    public static event OnItemUse onItemUsePrimairy = delegate { };
    public static event OnItemUse onItemUseSecundairy = delegate { };

    public UseableItem(UseableItem item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
    }

    public UseableItem(float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
    }

    public override Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new UseableItem((UseableItem)item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    public virtual void Select(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        OnItemSelected();
    }

    protected void OnItemSelected()
    {
        onItemSelected(this);
    }

    public virtual void Deselect(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        OnItemDeselected();
    }

    protected void OnItemDeselected()
    {
        onItemDeselected(this);
    }

    public virtual void UsePrimairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        OnItemUsePrimairy();
    }

    protected void OnItemUsePrimairy()
    {
        onItemUsePrimairy(this);
    }

    public virtual void UseSecondairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        onItemUseSecundairy(this);
    }

    protected void OnItemUseSecundairy()
    {
        onItemUseSecundairy(this);
    }
}
