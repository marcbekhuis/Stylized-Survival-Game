using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisplayVariables : MonoBehaviour
{
    [SerializeField] private InventoryDisplay inventoryDisplay;
    [SerializeField] private InventoryDisplay hotbarInventoryDisplay;
    [SerializeField] private HotbarDisplay hotbarDisplay;

    [SerializeField] private StatBar healthUI;
    [SerializeField] private StatBar foodUI;

    public void SetVariable(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        HotbarInventory hotbarInventory = player.GetComponent<HotbarInventory>();
        player.GetComponent<HealthSystem>().onHealthChanged += healthUI.UpdateBar;
        player.GetComponent<FoodSystem>().onFoodChanged += foodUI.UpdateBar;


        hotbarDisplay.hotbarInventory = hotbarInventory;
        hotbarDisplay.inventoryProp = hotbarInventory;
        inventoryDisplay.inventoryProp = inventory;
        hotbarInventoryDisplay.inventoryProp = hotbarInventory;
    }
}
