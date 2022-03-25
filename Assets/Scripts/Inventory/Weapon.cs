using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Items/Weapon")]
public class Weapon : UseableItem
{
    [Header("Weapon")]
    public float range = 2;
    public float damage = 2;
    public float cooldown = 1;
    public float durabilityDamage = 1;
    public LayerMask targetLayer;

    protected float cooldownTime;

    public Weapon(Weapon item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
        this.range = item.range;
        this.damage = item.damage;
        this.cooldown = item.cooldown;
        this.targetLayer = item.targetLayer;
        cooldownTime = GameManager.instance.gameTime;
    }

    public Weapon(LayerMask targetLayer, float range, float damage, float cooldown, float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
        this.range = range;
        this.damage = damage;
        this.cooldown = cooldown;
        this.targetLayer = targetLayer;
        cooldownTime = GameManager.instance.gameTime;
    }

    public override Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new Weapon((Weapon)item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    protected RaycastHit[] GetTargets()
    {
        Transform playerCamera = PlayerCamera.instance.transform;
        return Physics.RaycastAll(playerCamera.position, playerCamera.rotation * Vector3.forward, range, targetLayer);
    }

    public override void UsePrimairy(Transform userTransform, Transform itemTransform, Inventory[] inventories)
    {
        if (GameManager.instance.gameTime < cooldownTime) return;
        cooldownTime = GameManager.instance.gameTime + cooldown;

        OnItemUsePrimairy();
        Durability -= durabilityDamage;
        
        foreach (var target in GetTargets())
        {
            if (target.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.TakeDamage(damage, inventories);
                return;
            }
        }
    }
}