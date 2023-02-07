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

    private ItemPickUpSlot selectedItemSlot;

    public ItemPickUpSlot SelectedItemSlot {
        get => selectedItemSlot;
        set {
            selectedItemSlot = value;
            inventoryHighlight.SetParent(SelectedItemSlot);
        }
    }

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

	[SerializeField] List<ItemData> items;
	[SerializeField] GameObject itemPrefab;
	[SerializeField] Transform canvasTransform;

    Vector2Int oldPosition;
    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();

		if (Input.GetKeyDown(KeyCode.Q))
		{
            Debug.Log("work");
			if (selectedItem == null) 
            {
                CreateRandomItem();
            }
            else
            {
                selectedItem.itemData.itemIcon = null;
                Destroy(selectedItem);
                selectedItem = null;
            }
		}

        if (Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedItemGrid == null) 
        { 
            inventoryHighlight.Show(false);
            return; 
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedItemGrid != null)
            {
                Debug.Log("i decided to fucking kill myself");
			    LeftMouseButtonPress(selectedItemGrid);
            }
            else if (selectedItemSlot != null)
            {
                Debug.Log("yo");
                LeftMouseButtonPressSlot(selectedItemSlot);
            }
		}
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    private void InsertRandomItem()
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

    InventoryItem itemToHighlight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid) { return; }

        oldPosition = positionOnGrid;
        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
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

    private void LeftMouseButtonPress(ItemGrid itemGrid)
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

    private void LeftMouseButtonPressSlot(ItemPickUpSlot itemPickUpSlot)
    {
        Debug.Log("oy");
        if (selectedItem == null)
        {
            PickUpItemFromSlot();
        }
        else
        {
            PlaceItemInSlot();
        }
    }

    private void PickUpItemFromSlot()
    {
        selectedItem = selectedItemSlot.PickUpItem();
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void PlaceItemInSlot()
    {
        selectedItemSlot.PlaceItem(selectedItem, ref overlapItem);
        selectedItem = null;
        if (overlapItem != null)
        {
            selectedItem = overlapItem;
            overlapItem = null;
            rectTransform = selectedItem.GetComponent<RectTransform>();
            rectTransform.SetAsLastSibling();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
