using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueCounter : MonoBehaviour
{
    InventoryController inventoryController;
    ItemGrid itemGrid;
    public static int totalValue = 0;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void AddValue(int valueToAdd)
    {
        totalValue += valueToAdd;
    }

    public void SubtractValue(int valueToSubtract)
    {
        totalValue -= valueToSubtract;
    }
}
