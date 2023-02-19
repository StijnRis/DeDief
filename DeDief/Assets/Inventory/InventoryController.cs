using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public CanvasScaler canvasScaler;
    public RectTransform canvasRectTransform;

    Vector2Int oldPosition;
    public InventoryHighlight inventoryHighlight;

    public GameObject player;

    public bool placeItemMode = false;
    Vector3 point;
    Color originalItemColor;

    public static Vector2 screenScale;

    public GameObject panel;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        screenScale = new Vector2(canvasScaler.referenceResolution.x / Screen.width, canvasScaler.referenceResolution.y / Screen.height);
        player = gameObject.transform.parent.gameObject;
        panel = GameObject.FindGameObjectWithTag("InventoryPanel");
        panel.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInteract.inventoryOpen)
        {
            ItemIconDrag();

            if (placeItemMode && selectedItem != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                point = ray.origin + ray.direction;
                Debug.DrawRay(ray.origin, ray.direction);
                // Debug.Log(point);
                selectedItem.item3d.transform.position = point;
                Destroy(selectedItem.item3d.GetComponent<Rigidbody>());
                selectedItem.item3d.transform.rotation = player.transform.rotation;
                selectedItem.item3d.SetActive(true);
            } 
            else if (selectedItem != null)
            {
                selectedItem.item3d.SetActive(false);
            }

            if (selectedItemGrid == null) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // CloseInventory();
                    if (selectedItem != null)
                    {
                        // if (!selectedItem.pickedUp)
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
        // GameObject item3dPrefab = item.itemData.itemPrefab;
        // GameObject item3d = Instantiate(item3dPrefab, player.transform.position + (transform.forward), player.transform.rotation);
        GameObject item3d = item.item3d;
        // item3d.transform.SetParent(GameObject.FindGameObjectWithTag("Office").transform);
        item3d.transform.position = point;
        item3d.transform.rotation = player.transform.rotation;
        item3d.AddComponent(typeof(Rigidbody));
        // item3d.SetActive(true);
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
                // Debug.Log("Highlight may show");
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
		rectTransform.SetParent(canvasTransform, false);
        rectTransform.SetAsLastSibling();
		inventoryItem.Set(itemData);
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

        // if (selectedItemGrid != null)
        // {
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(selectedItemGrid.rectTransform, Input.mousePosition, null, out position);
        // }

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        // Debug.Log(selectedItemGrid.GetTileGridPosition(position));
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
        panel.SetActive(inventoryOpen);
        Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
