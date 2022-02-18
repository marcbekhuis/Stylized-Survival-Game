using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New tool", menuName = "Items/Tool")]
public class Tool : Weapon
{
    [Header("Tool")]
    [SerializeField] private ResourceTypes resourceType;

    public Tool(Weapon item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
    }

    public Tool(float range, float damage, float cooldown, float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(range, damage, cooldown, durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
    }

    public override void UsePrimairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        if (GameManager.instance.gameTime < cooldownTime) return;
        cooldownTime = GameManager.instance.gameTime + cooldown;

        OnItemUsePrimairy();

        foreach (var target in GetTargets())
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
                return;
            }
        }
    }
}
