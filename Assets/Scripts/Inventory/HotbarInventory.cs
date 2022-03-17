using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarInventory : Inventory
{
    [SerializeField] private InputActions primairyAction;
    [SerializeField] private InputActions secondairyAction;
    [SerializeField] private InputActions[] inputActions;
    [SerializeField] private Transform itemTransform;

    public Item selectedItem
    {
        get => slots[index2d.x, index2d.y];
    }

    public Vector2Int index2d = Vector2Int.zero;
    public int index = 0;

    private new void Start()
    {
        if (!photonView.IsMine) return;
        base.Start();
        SelectItem(false);
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        if (GameManager.instance.inventoryOpen) return;

        if (selectedItem)
        {
            if (InputManager.instance.GetKeyDown(primairyAction))
            {
                UseableItem item = selectedItem.GetType<UseableItem>();
                if (item)
                {
                    item.UsePrimairy(transform, itemTransform, new Inventory[] { this });
                }
            }
            if (InputManager.instance.GetKeyDown(secondairyAction))
            {
                UseableItem item = selectedItem.GetType<UseableItem>();
                if (item)
                {
                    item.UseSecondairy(transform, itemTransform, new Inventory[] { this });
                }
            }
        }

        for (int i = 0; i < inputActions.Length; i++)
        {
            if (InputManager.instance.GetKeyDown(inputActions[i]))
            {
                DeselectItem();
                SetIndex(i);
                SelectItem();

                OnInventoryChanged();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Transform playerCamera = PlayerCamera.instance.transform;
        Gizmos.DrawLine(playerCamera.position, playerCamera.position + playerCamera.rotation * Vector3.forward * 10);
    }

    public void DeselectItem() 
    {
        if (selectedItem)
        {
            UseableItem item = selectedItem.GetType<UseableItem>();
            if (item)
            {
                item.Deselect(transform, itemTransform, new Inventory[] { this });
            }
        }
    }

    public void SetIndex(int i)
    {
        int x = i;
        int y;
        for (y = 0; y < Mathf.FloorToInt((float)i / (float)slots.GetLength(0)); y++)
        {
            x -= slots.GetLength(0);
        }
        index2d = new Vector2Int(x, y);
        index = i;
    }

    public void SelectItem(bool triggerSelect = true)
    {
        if (selectedItem)
        {
            if (triggerSelect)
            {
                UseableItem item = selectedItem.GetType<UseableItem>();
                if (item)
                {
                    item.Select(transform, transform, new Inventory[] { this });
                }
            }
        }
    }

    public override Item RemoveItem(Item item)
    {
        if (selectedItem == item) DeselectItem();
        Item removedItem = base.RemoveItem(item);
        return removedItem;
    }

    public override Item RemoveItem(Vector2Int index)
    {
        if (selectedItem == GetSlotItem(index)) DeselectItem();
        Item removedItem = base.RemoveItem(index);
        return removedItem;
    }

    public override Item AddItem(Item item)
    {
        Item addedItem = base.AddItem(item);
        if (GetItemIndex(addedItem) == index) SelectItem();
        return addedItem;
    }

    public override Item AddItem(Item item, int amount)
    {
        Item addedItem = base.AddItem(item, amount);
        if (GetItemIndex(addedItem) == index) SelectItem();
        return addedItem;
    }
    
    public override Item AddItem(Item item, Vector2Int index)
    {
        Item addedItem = base.AddItem(item, index);
        if(index == index2d) SelectItem();
        return addedItem;
    }

    private int GetItemIndex(Item item)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if(slots[x,y] == item)
                {
                    return y * size.x + x;
                }
            }
        }
        return 0;
    }
}
