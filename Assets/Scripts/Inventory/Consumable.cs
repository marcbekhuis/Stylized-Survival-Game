using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : UseableItem
{
    [Header("Food")]
    public float foodRegen = 1;
    public float healthRegen = 1;
    public float cooldown = 1;

    protected float cooldownTime;

    public Consumable(Consumable item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
        this.foodRegen = item.foodRegen;
        this.healthRegen = item.healthRegen;
        this.cooldown = item.cooldown;
    }

    public Consumable(float foodRegen, float healthRegen, float cooldown, float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
        this.foodRegen = foodRegen;
        this.healthRegen = healthRegen;
        this.cooldown = cooldown;
    }

    public override Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new Consumable((Consumable)item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    public override void UseSecondairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        if (GameManager.instance.gameTime < cooldownTime) return;
        cooldownTime = GameManager.instance.gameTime + cooldown;
        OnItemUsePrimairy();

        userTransform.GetComponent<HealthSystem>().Health += healthRegen;
        userTransform.GetComponent<FoodSystem>().Food += foodRegen;
    }
}
