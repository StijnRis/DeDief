using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueCounter : MonoBehaviour
{
    InventoryController inventoryController;
    ItemGrid itemGrid;
    public TextMeshProUGUI valueText;
    public int totalValue = 0;
    public int addToTotalEverStolen = 0;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    private void Update()
    {
        valueText.text = "Total value: €" + totalValue.ToString();
    }

    public void AddValue(int valueToAdd)
    {
        totalValue += valueToAdd;
        addToTotalEverStolen += valueToAdd;
    }

    public void SubtractValue(int valueToSubtract)
    {
        totalValue -= valueToSubtract;
        addToTotalEverStolen -= valueToSubtract;
    }
}
