using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid { 
        get => selectedItemGrid; 
        set { 
            selectedItemGrid = value;
            inventoryHighlight.SetParent(SelectedItemGrid);
        }
    }

    InventoryItem selectedItem;
    public InventoryItem SelectedItem {
        get => selectedItem;
    }
    InventoryItem overlapItem;
    RectTransform rectTransform;

	[SerializeField] List<ItemData> items;
	[SerializeField] GameObject itemPrefab;
	public GameObject canvas;
    private Transform canvasTransform;

    Vector2Int oldPosition;
    public InventoryHighlight inventoryHighlight;

    public GameObject player;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        player = gameObject.transform.parent.gameObject;
    }

    private void Update()
    {
        if (PlayerInteract.inventoryOpen)
        {
            ItemIconDrag();

            if (selectedItemGrid == null) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // CloseInventory();
                    if (selectedItem != null)
                    {
                        if (!selectedItem.pickedUp)
                            DropItem(selectedItem);
                    } 
                    else 
                    {
                        PlayerInteract.inventoryOpen = false;
                        SetInventoryActive(PlayerInteract.inventoryOpen);
                    }
                }
                inventoryHighlight.Show(false);
                return; 
            }

            HandleHighlight();

            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonPress();
            }
        }
    }

    private void DropItem(InventoryItem item)
    {
        GameObject item3dPrefab = item.itemData.itemPrefab;
        GameObject item3d = Instantiate(item3dPrefab, player.transform.position + (transform.forward), player.transform.rotation);
        item3d.AddComponent(typeof(Rigidbody));
        Destroy(item.gameObject);
    }

    private void CloseInventory()
    {
        inventoryHighlight.SetObjectAsParent(canvasTransform);    
        inventoryHighlight.Show(false);
        PlayerInteract.inventoryOpen = false;
        PickUpInteract[] pickUpGrids = canvas.GetComponentsInChildren<PickUpInteract>();
        foreach (PickUpInteract pickUpInteract in pickUpGrids)
        {
            Destroy(pickUpInteract.gameObject);
        }
        // SetInventoryActive(PlayerInteract.inventoryOpen);
    }

    public void GenerateRandomItem()
    {
        if (selectedItem == null) 
        {
            CreateRandomItem();
        }
    }

    public void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    public void InsertRandomItem()
    {
        if (selectedItemGrid == null) { return; }
        
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null) { return; }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    public void InsertItem(InventoryItem itemToInsert, ItemGrid itemGrid)
    {
        Vector2Int? posOnGrid = itemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null) { return; }

        itemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    InventoryItem itemToHighlight;

    private void HandleHighlight()
    {
        // Debug.Log("Highlight Handled");
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid) { return; }

        oldPosition = positionOnGrid;
        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                Debug.Log("Highlight may show");
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else 
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(
                positionOnGrid.x, 
                positionOnGrid.y, 
                selectedItem.WIDTH, 
                selectedItem.HEIGHT
                ));
            
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null && (itemToHighlight.WIDTH > selectedItem.WIDTH || itemToHighlight.HEIGHT > selectedItem.HEIGHT))
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.SetSize(selectedItem);
                inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
            }
        }
    }

    public InventoryItem CreateItem(ItemData itemData)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        rectTransform = inventoryItem.GetComponent<RectTransform>();
		rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();
		inventoryItem.Set(itemData);
        inventoryItem.pickedUp = true;
        return inventoryItem;
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
		selectedItem = inventoryItem;

		rectTransform = inventoryItem.GetComponent<RectTransform>();
		rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

		int selectedItemID = UnityEngine.Random.Range(0,items.Count);
		inventoryItem.Set(items[selectedItemID]);
    }

    public void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }  
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    public void SetInventoryActive(bool inventoryOpen)
    {
        if (inventoryOpen == false)
        {
            CloseInventory();
        }
        canvas.SetActive(inventoryOpen);
        Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
