using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInventoryDisplayVariables : MonoBehaviour
{
    [SerializeField] private InventoryDisplay inventoryDisplay;
    [SerializeField] private InventoryDisplay hotbarInventoryDisplay;
    [SerializeField] private HotbarDisplay hotbarDisplay;

    [SerializeField] private HealthUI healthUI;

    public void SetVariable(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        HotbarInventory hotbarInventory = player.GetComponent<HotbarInventory>();
        //player.GetComponent<HealthSystem>().onHealthChanged += healthUI.updateHealth;


        hotbarDisplay.hotbarInventory = hotbarInventory;
        hotbarDisplay.inventoryProp = hotbarInventory;
        inventoryDisplay.inventoryProp = inventory;
        hotbarInventoryDisplay.inventoryProp = hotbarInventory;


    }
}
