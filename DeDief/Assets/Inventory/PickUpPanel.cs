using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpPanel : MonoBehaviour
{
    public ItemGrid pickUpGrid;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI valueText;
    Vector2 panelSize;

    void Awake()
    {
        pickUpGrid = transform.GetChild(0).GetComponent<ItemGrid>();
        itemTitle = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        valueText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void Init(InventoryItem item, int width, int height, int moneyValue, string name, GameObject itemObject)
    {
        itemTitle.text = name;
        valueText.text = "Worth: €" + moneyValue.ToString();
        pickUpGrid.Init(width, height);
        pickUpGrid.GetComponent<PickUpInteract>().itemObject = itemObject;
        pickUpGrid.GetComponent<PickUpInteract>().item = item;
        panelSize = new Vector2(width * ItemGrid.tileSizeWidth + 200, height * ItemGrid.tileSizeHeight + 200) * InventoryController.screenScale;
        GetComponent<RectTransform>().sizeDelta = panelSize;

        RectTransform gridRect = pickUpGrid.GetComponent<RectTransform>();
        gridRect.anchoredPosition = new Vector2(100, -100) * InventoryController.screenScale;
        
        RectTransform titleRect = itemTitle.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, -60);

        RectTransform valueRect = valueText.GetComponent<RectTransform>();
        valueRect.anchoredPosition = new Vector2(0, 40);
        // pickUpGrid.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
        // pickUpGrid.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
