using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftingItem", menuName = "Crafting/Item")]
public class CraftingItem : ScriptableObject
{
    public ItemWithAmount[] requiredItems;
    public ItemWithAmount[] rewardItems;
}
