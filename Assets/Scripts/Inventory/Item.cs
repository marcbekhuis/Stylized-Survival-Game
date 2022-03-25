using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New item" , menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [Header("Item")]
    public string description;
    [Min(0)] public float weight = 1;
    [Min(1)] public int maxStack = 1;
    public Sprite icon;
    public Sprite sprite;

    [HideInInspector] private int amount = 0;
    [HideInInspector] public Inventory inventory;

    public delegate void AmountUpdate(int amount);
    public event AmountUpdate OnAmountUpdate;

    public Item(string description, float weight, int maxStack, Sprite icon, Sprite sprite, Inventory inventory, int amount = 1)
    {
        this.description = description;
        this.weight = weight;
        this.maxStack = maxStack;
        this.icon = icon;
        this.sprite = sprite;
        this.inventory = inventory;
        this.amount = amount;
    }

    public Item(Item item, Inventory inventory, int amount = 1)
    {
        this.description = item.description;
        this.weight = item.weight;
        this.maxStack = item.maxStack;
        this.icon = item.icon;
        this.sprite = item.sprite;
        this.inventory = inventory;
        this.amount = amount;
    }

    public int Amount { 
        get => amount; 
        set
        {
            amount = Mathf.Clamp(value, 0, maxStack);
            OnAmountUpdate(amount);
        } 
    }

    public virtual Item Create(Item item, Inventory inventory, int amount = 1)
    {
        Item newItem = new Item(item, inventory, amount);
        newItem.name = this.name;
        return newItem;
    }

    public virtual string GetTooltipText()
    {
        return this.name + "\n" +
            "\n" + description + "\n" +
            "\n" + "Amount: " + amount + "/" + maxStack + 
            "\n" + "Weight: " + weight * amount;
    }

    public T GetType<T>() where T : Item
    {
        try
        {
            return (T)this;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool IsType<T>() where T : Item
    {
        try
        {
            return (T)this;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
