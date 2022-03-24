using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : Singleton<CraftingManager>
{
    [SerializeField] private GameObject craftingItemPrefab;
    [SerializeField] private Transform craftingParent;
    [SerializeField] private ItemContainer itemContainerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var craftingItem in CraftingList.instance.craftingItems)
        {
            GameObject craftingItemObject = Instantiate(craftingItemPrefab, craftingParent);
            Transform requiredItemsObject = craftingItemObject.transform.Find("Required Items");
            Transform rewardItemsObject = craftingItemObject.transform.Find("Reward Items");

            foreach (var requiredItem in craftingItem.requiredItems)
            {
                ItemContainer itemContainer = Instantiate(itemContainerPrefab, requiredItemsObject);
                itemContainer.Setup(Vector2Int.zero, false, null, false);
                itemContainer.item = requiredItem.item.Create(requiredItem.item, null, requiredItem.amount);
            }

            foreach (var rewardItem in craftingItem.rewardItems)
            {
                ItemContainer itemContainer = Instantiate(itemContainerPrefab, rewardItemsObject);
                itemContainer.Setup(Vector2Int.zero, false, null, false);
                itemContainer.item = rewardItem.item.Create(rewardItem.item, null, rewardItem.amount);
            }
        }
    }
}
