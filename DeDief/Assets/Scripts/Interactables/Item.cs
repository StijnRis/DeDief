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
    [SerializeField] GameObject itemGrid;
    public int width = 1;
	public int height = 1;
	public Sprite itemIcon;
	public bool canBeRotated;

    public Vector3 oldPosition;
	public Quaternion oldRotation;

    InventoryController inventoryController;
    RectTransform inventoryRectTransform;
    private ItemData data;
    // data.InitItem(width, height, itemIcon, canBeRotated, moneyValue, gameObject);
    protected InventoryItem item;
    System.Random random = new System.Random();

    public void Awake()
    {
        data = (ItemData) ScriptableObject.CreateInstance(typeof(ItemData));
        int randomizer = UnityEngine.Random.Range(0,3);
		/*Debug.Log(randomizer);*/
		double variance = moneyValue * 0.2;
		if (variance < 1)
			variance = 1;
		if (randomizer == 1)
			moneyValue = Convert.ToInt32(Convert.ToDouble(moneyValue) + random.NextDouble() * variance);
		else if (randomizer == 2)
			moneyValue = Convert.ToInt32(Convert.ToDouble(moneyValue) - random.NextDouble() * variance);
/*        Debug.Log(moneyValue);
*/        data.InitItem(width, height, itemIcon, canBeRotated, moneyValue, gameObject);
        promptMessage = "Pick up " + name;
        
    }

    protected override void Interact()
    {
        // get inventory controller and inventory canvas
        inventoryController = Camera.main.GetComponent<InventoryController>();
        inventoryRectTransform = inventoryController.canvas.GetComponent<RectTransform>();

        if (!PlayerInteract.inventoryOpen)
        {
            // Debug.Log("inventory created");

            // Freeze the player
            PlayerInteract.inventoryOpen = !PlayerInteract.inventoryOpen;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);
            // Debug.Log("Player frozen");

            // create new grid with dimensions of item and move it to the inventory canvas
            oldPosition = transform.position;
            oldRotation = transform.rotation;
            GameObject pickUpGrid = Instantiate(itemGrid) as GameObject;
            pickUpGrid.GetComponent<RectTransform>().SetParent(inventoryRectTransform);
            pickUpGrid.GetComponent<RectTransform>().localPosition = new Vector2(-900,300);
            pickUpGrid.GetComponent<PickUpInteract>().itemObject = gameObject;

            ItemGrid gridScript = pickUpGrid.GetComponent<ItemGrid>();
            gridScript.gridSizeWidth = width;
            gridScript.gridSizeHeight = height;
            gridScript.Init(width, height);
            // Debug.Log("inventory rendered");

            // insert item into grid
            item = inventoryController.CreateItem(data);
            item.item3d = gameObject;
            pickUpGrid.GetComponent<PickUpInteract>().item = item;
            inventoryController.InsertItem(item, gridScript);
            // Debug.Log("item inserted");
            // Destroy(gameObject);
        }
        else
        {
            PlayerInteract.inventoryOpen = false;
            inventoryController.SetInventoryActive(PlayerInteract.inventoryOpen);
        }
    }

    public void ResetPosition()
    {
        transform.position = oldPosition;
        transform.rotation = oldRotation;
    }
}
