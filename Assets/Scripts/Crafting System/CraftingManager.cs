using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [SerializeField] private GameObject craftingItemPrefab;
    [SerializeField] private Transform craftingParent;
    [SerializeField] private ItemContainer itemContainerPrefab;

    public List<Inventory> inventories = new List<Inventory>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var craftingItem in CraftingList.instance.craftingItems)
        {
            GameObject craftingItemObject = Instantiate(craftingItemPrefab, craftingParent);
            Transform requiredItemsObject = craftingItemObject.transform.Find("Required Items");
            Transform rewardItemsObject = craftingItemObject.transform.Find("Reward Items");
            craftingItemObject.GetComponent<Button>().onClick.AddListener(() => CraftItem(craftingItem));

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

    private void CraftItem(CraftingItem craftingItem)
    {
        foreach (var requiredItem in craftingItem.requiredItems)
        {
            bool anInventoryHas = false;
            foreach (var inventory in inventories)
            {
                if (inventory.GetItemAmount(requiredItem.item) >= requiredItem.amount) anInventoryHas = true;
            }
            if (!anInventoryHas) return;
        }

        foreach (var requiredItem in craftingItem.requiredItems)
        {
            int amount = requiredItem.amount;
            foreach (var inventory in inventories)
            {
                amount = inventory.RemoveItem(requiredItem.item, amount);
                if (amount <= 0) break;
            }
        }
        foreach (var rewardItem in craftingItem.rewardItems)
        {
            foreach (var inventory in inventories)
            {
                if (inventory.HasSpace())
                {
                    inventory.AddItem(rewardItem.item, rewardItem.amount);
                    return;
                }
            }
        }
    }
}
