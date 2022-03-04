using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New tool", menuName = "Items/Tool")]
public class Tool : Weapon
{
    [Header("Tool")]
    [SerializeField] private ResourceTypes resourceType;

    public Tool(Tool item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
        this.resourceType = item.resourceType;
    }

    public Tool(ResourceTypes resourceType, LayerMask targetLayer, float range, float damage, float cooldown, float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(targetLayer, range, damage, cooldown, durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
        this.resourceType = resourceType;
    }

    public override Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new Tool((Tool)item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    public override void UsePrimairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        if (GameManager.instance.gameTime < cooldownTime) return;
        cooldownTime = GameManager.instance.gameTime + cooldown;
        OnItemUsePrimairy();

        foreach (var target in base.GetTargets())
        {
            if (target.transform.TryGetComponent<Resource>(out Resource resource))
            {
                if (resource.CanHarvest(resourceType))
                {
                    resource.Harvest(inventories);
                    return;
                }
            }
            if (target.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.Health -= damage;
                return;
            }
        }
    }
}
