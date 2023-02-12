using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] ItemData itemData;
    [SerializeField] GameObject itemGrid;
    InventoryController inventoryController;
    RectTransform inventoryRectTransform;

    protected override void Interact()
    {
        // get inventory controller and inventory canvas
        inventoryController = Camera.main.GetComponent<InventoryController>();
        inventoryRectTransform = inventoryController.canvas.GetComponent<RectTransform>();

        if (!PlayerInteract.inventoryOpen)
        {
            Debug.Log("inventory created");

            // Freeze the player
            PlayerInteract.inventoryOpen = !PlayerInteract.inventoryOpen;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);
            Debug.Log("Player frozen");

            // create new grid with dimensions of item and move it to the inventory canvas
            GameObject pickUpGrid = Instantiate(itemGrid) as GameObject;
            pickUpGrid.GetComponent<RectTransform>().SetParent(inventoryRectTransform);
            pickUpGrid.GetComponent<RectTransform>().localPosition = new Vector2(-900,300);
            pickUpGrid.GetComponent<PickUpInteract>().itemObject = gameObject;
            
            ItemGrid gridScript = pickUpGrid.GetComponent<ItemGrid>();
            gridScript.gridSizeWidth = itemData.width;
            gridScript.gridSizeHeight = itemData.height;
            gridScript.Init(itemData.width, itemData.height);
            Debug.Log("inventory rendered");

            // insert item into grid
            InventoryItem item = inventoryController.CreateItem(itemData);
            pickUpGrid.GetComponent<PickUpInteract>().item = item;
            inventoryController.InsertItem(item, gridScript);
            Debug.Log("item inserted");
            // Destroy(gameObject);
        }
        else
        {
            PlayerInteract.inventoryOpen = false;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);
        }
    }
}
