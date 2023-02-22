using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
	public const float tileSizeWidth = 64;
	public const float tileSizeHeight = 64;
	public Vector2 tileSize = new Vector2(tileSizeWidth, tileSizeHeight);

	InventoryItem[,] inventoryItemSlot;

	RectTransform rectTransform;

	public int gridSizeWidth = 20;
	public int gridSizeHeight = 10;

	ValueCounter valueCounter;
	bool hasValueCounter = false;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		if (GetComponent<ValueCounter>() != null)
		{
			valueCounter = GetComponent<ValueCounter>();
			hasValueCounter = true;
		}
		Init(gridSizeWidth, gridSizeHeight);
	}

	public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
		
		if (hasValueCounter)
		{
			valueCounter.SubtractValue(item.itemData.moneyValue);
		}
    }

    public void Init(int width, int height)
	{
		inventoryItemSlot = new InventoryItem[width, height];
		Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
		rectTransform.sizeDelta = size;
	}

	Vector2 positionOnGrid = new Vector2();
	Vector2Int tileGridPosition = new Vector2Int();
	public Vector2Int GetTileGridPosition(Vector2 mousePosition)
	{
		positionOnGrid.x = mousePosition.x - rectTransform.position.x - 0.001f;
		positionOnGrid.y = rectTransform.position.y - mousePosition.y - 0.001f;

		tileGridPosition.x = (int)(positionOnGrid.x / tileSize.x * InventoryController.screenScale.x);
		tileGridPosition.y = (int)(positionOnGrid.y / tileSize.y * InventoryController.screenScale.y);

		if (tileGridPosition.x < 0)
			tileGridPosition.x = 0;

		if (tileGridPosition.y < 0)
			tileGridPosition.y = 0;

		return tileGridPosition;
	}

	public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
		rectTransform.SetAsLastSibling();

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
		inventoryItem.grid = this;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;

		if (hasValueCounter)
		{
			valueCounter.AddValue(inventoryItem.itemData.moneyValue);
		}
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x=0; x<width; x++)
		{
			for (int y=0; y<height; y++)
			{
				if (inventoryItemSlot[posX+x, posY+y] !=null)
				{
					if (overlapItem == null)
					{
						overlapItem = inventoryItemSlot[posX+x, posY+y];
					}
					else 
					{
						if (overlapItem != inventoryItemSlot[posX+x, posY+y])
						{
							return false;
						}
					}
				}
			}
		}

		return true;
    }

	private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x=0; x<width; x++)
		{
			for (int y=0; y<height; y++)
			{
				if (inventoryItemSlot[posX+x, posY+y] !=null)
				{
					return false;
				}
			}
		}

		return true;
    }

    bool PositionCheck(int posX, int posY)
	{
		if (posX < 0 || posY < 0)
		{
			return false;
		}

		if (posX >= gridSizeWidth || posY >= gridSizeHeight)
		{
			return false;
		}

		return true;
	}

	public bool BoundryCheck(int posX, int posY, int width, int height)
	{
		if (PositionCheck(posX, posY) == false) { return false; }

		posX += width-1;
		posY += height-1;

		if (PositionCheck(posX, posY) == false) { return false; }


		
		return true;
	}

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x,y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
		int height = gridSizeHeight - itemToInsert.HEIGHT + 1;
		int width = gridSizeWidth - itemToInsert.WIDTH + 1;
        for (int y=0; y < height; y++)
		{
			for (int x=0; x < width; x++)
			{
				if (CheckAvailableSpace(x,y, itemToInsert.WIDTH, itemToInsert.HEIGHT))
				{
					return new Vector2Int(x,y);
				}
			}
		}

		return null;
    }
}
