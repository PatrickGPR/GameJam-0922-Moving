using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Item currentItem;
    
    public bool HasItem => currentItem != null;

    public Item CurrentItem { get => currentItem; set => currentItem = value; }

    public Action<Item> OnItemDrop;
    public Action<Item> OnItemPickup;

    public void PickupItem(Item item)
    {
        //logic 
        if (HasItem)
        {
            return;
        }

        currentItem = item;
        OnItemPickup?.Invoke(currentItem);
    }

    public void DropItem()
    {
        if (!HasItem)
        {
            return;
        }

        OnItemDrop?.Invoke(currentItem);
        currentItem.transform.parent = null;
        currentItem = null;
    }
}
