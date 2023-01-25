using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    [SerializeField] int gridSizeWidth = 20;
    [SerializeField] int gridSizeHeight = 10;

    [SerializeField] GameObject inventoryItemPrefab;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
        
        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 1, 1);

        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 5, 2);

        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 3, 7);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x,y];
        inventoryItemSlot[x,y] = null;
        return toReturn;
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        inventoryItemSlot[posX, posY] = inventoryItem.GetComponent<InventoryItem>();

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + (tileSizeWidth / 2);
        position.y = -(posY * tileSizeHeight + (tileSizeHeight / 2));

        rectTransform.localPosition = position;
    }
}
