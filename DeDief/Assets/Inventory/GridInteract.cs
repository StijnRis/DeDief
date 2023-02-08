using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        inventoryController.SelectedItemGrid = itemGrid;
        itemGrid.GetComponent<RectTransform>().SetAsFirstSibling();
        // inventoryController.inventoryHighlight.transform.SetAsFirstSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("leave");
        inventoryController.SelectedItemGrid = null;
    }
}
