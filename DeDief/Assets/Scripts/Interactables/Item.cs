using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Item : Interactable
{
    [Header("Item settings")]
    public new string name = "Item";
    public int moneyValue;
    
    [Header("In grid settings")]
    // [SerializeField] protected GameObject itemGrid;
    [SerializeField] protected GameObject itemPanel;
    public int width = 1;
	public int height = 1;
	public Sprite itemIcon;
	public bool canBeRotated;

    public Vector3 oldPosition;
	public Quaternion oldRotation;

    protected InventoryController inventoryController;
    protected RectTransform inventoryRectTransform;
    protected GameObject canvas;
    protected ItemData data;
    // data.InitItem(width, height, itemIcon, canBeRotated, moneyValue, gameObject);
    public InventoryItem item;
    protected GameObject item3d;
    protected System.Random random = new System.Random();

    protected void Awake()
    {
        data = (ItemData) ScriptableObject.CreateInstance(typeof(ItemData));

        int randomizer = UnityEngine.Random.Range(0,6);
		/*Debug.Log(randomizer);*/
		double variance = moneyValue * 0.2;
		if (variance < 1)
			variance = 1;
		if (randomizer <= 2)
			moneyValue = Convert.ToInt32(Convert.ToDouble(moneyValue) + random.NextDouble() * variance);
		else if (randomizer <= 4)
			moneyValue = Convert.ToInt32(Convert.ToDouble(moneyValue) - random.NextDouble() * variance);
        data.InitItem(name, width, height, itemIcon, canBeRotated, moneyValue, gameObject);
        promptTitle = "Pick up " + name;
        promptDescription =  "Worth €" + moneyValue.ToString();
        item3d = gameObject;
    }

    protected override void Interact()
    {
        // get inventory controller and inventory canvas
        inventoryController = Camera.main.GetComponent<InventoryController>();
        inventoryRectTransform = inventoryController.canvas.GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("InventoryCanvas");

        if (!PlayerInteract.inventoryOpen)
        {
            // Freeze the player
            PlayerInteract.inventoryOpen = !PlayerInteract.inventoryOpen;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);

            CreatePickUpGrid();
        }
        else
        {
            PlayerInteract.inventoryOpen = false;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);
        }
    }

    protected void CreatePickUpGrid()
    {
        // create new grid with dimensions of item and move it to the inventory canvas
        oldPosition = transform.position;
        oldRotation = transform.rotation;
        // GameObject pickUpGrid = Instantiate(itemGrid) as GameObject;
        // pickUpGrid.GetComponent<RectTransform>().SetParent(inventoryRectTransform, false);
        // pickUpGrid.GetComponent<RectTransform>().localPosition = new Vector2(-900, 300);
        // pickUpGrid.GetComponent<PickUpInteract>().itemObject = gameObject;

        // ItemGrid gridScript = pickUpGrid.GetComponent<ItemGrid>();
        // gridScript.gridSizeWidth = width;
        // gridScript.gridSizeHeight = height;
        // gridScript.Init(width, height);

        item = inventoryController.CreateItem(data);
        item.item3d = item3d;
        GameObject pickUpPanel = Instantiate(itemPanel) as GameObject;
        pickUpPanel.GetComponent<RectTransform>().SetParent(inventoryRectTransform, false);
        pickUpPanel.GetComponent<PickUpPanel>().Init(item, width, height, moneyValue, name, gameObject);
        ItemGrid gridScript = pickUpPanel.GetComponent<PickUpPanel>().pickUpGrid; 
        GameObject pickUpGrid = gridScript.gameObject;

        // insert item into grid
        inventoryController.InsertItem(item, gridScript);
    }

    public void ResetPosition()
    {
        transform.position = oldPosition;
        transform.rotation = oldRotation;
    }
}
