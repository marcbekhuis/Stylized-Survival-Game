using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInventoryDisplayVariables : MonoBehaviour
{
    [SerializeField] private InventoryDisplay inventoryDisplay;
    [SerializeField] private InventoryDisplay hotbarInventoryDisplay;
    [SerializeField] private HotbarDisplay hotbarDisplay;

    public void SetVariable(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        HotbarInventory hotbarInventory = player.GetComponent<HotbarInventory>();

        hotbarDisplay.hotbarInventory = hotbarInventory;
        hotbarDisplay.inventoryProp = hotbarInventory;
        inventoryDisplay.inventoryProp = inventory;
        hotbarInventoryDisplay.inventoryProp = hotbarInventory;
    }
}
