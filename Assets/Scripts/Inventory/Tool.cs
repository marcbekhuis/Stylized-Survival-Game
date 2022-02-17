using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Weapon
{
    public Tool(Weapon item, Inventory inventory, int amount = 1) : base(item, inventory, amount)
    {
    }

    public Tool(float range, float damage, float cooldown, float durability, float maxDurability, string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1) : base(range, damage, cooldown, durability, maxDurability, description, weight, maxStack, icon, sprite, inventory, amount)
    {
    }
}
