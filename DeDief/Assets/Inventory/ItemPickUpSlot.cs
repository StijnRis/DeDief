using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpSlot : MonoBehaviour
{
    [SerializeField] InventoryItem inventoryItem;

    RectTransform rectTransform;
    
    private void onEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(inventoryItem);
    }
    
    private void Init(InventoryItem pickedUpItem)
    {
        if (pickedUpItem == null) 
        {
            inventoryItem = null;
            return;
        }

        PlaceItem(pickedUpItem);
    }

    public InventoryItem PickUpItem()
    {
        InventoryItem toReturn = inventoryItem;
        inventoryItem = null;
        return toReturn;
    }

    public void PlaceItem(InventoryItem item)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        inventoryItem = item;
    }

    public void PlaceItem(InventoryItem item, ref InventoryItem overlapItem)
    {
        if (inventoryItem != null)
        {
            overlapItem = inventoryItem;
        }
        
        PlaceItem(item);
    }
}