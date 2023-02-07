using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;

    ItemPickUpSlot itemPickUpSlot;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemPickUpSlot = GetComponent<ItemPickUpSlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter slot");
        inventoryController.SelectedItemSlot = itemPickUpSlot;
        itemPickUpSlot.GetComponent<RectTransform>().SetAsFirstSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("leave slot");
        inventoryController.SelectedItemSlot = null;
    }
}
