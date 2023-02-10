using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;
    public GameObject itemObject;
    public InventoryItem item;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.hierarchyCount);
            if (itemGrid.GetItem(0,0) == null && transform.childCount == 0 && inventoryController.SelectedItem == null)
            {
                item.pickedUp = false;
                Destroy(gameObject);
                Destroy(itemObject);
            }
        }
    }
}
