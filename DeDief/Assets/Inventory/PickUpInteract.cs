using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpInteract : GridInteract
{
    public GameObject itemObject;
    public InventoryItem item;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log(transform.hierarchyCount);
            if (itemGrid.GetItem(0,0) == null)
            {
                if (transform.childCount == 0) {
                    if (itemObject != itemObject.GetComponent<Item>().item.item3d)
                        Destroy(itemObject);
                    // Destroy(gameObject);
                }
                // itemObject.SetActive(false);
            }
        }
        if (itemGrid.GetItem(0,0) != null) 
        {
            if (itemGrid.GetItem(0,0) == item)
            {
                if (itemObject != itemObject.GetComponent<Item>().item.item3d) 
                {
                    itemObject.GetComponent<Item>().item.item3d.SetActive(true);
                }         
                else
                {
                    itemObject.SetActive(true);
                }
            }
            else
            {
                itemGrid.GetItem(0,0).item3d.transform.position = itemObject.transform.position;
                itemGrid.GetItem(0,0).item3d.SetActive(true);
            }
        }
        // for (int x = 0; x < itemGrid.gridSizeWidth; x++)
        // {
        //     for (int y = 0; y < itemGrid.gridSizeHeight; y++)
        //     {
        //         Debug.Log(x.ToString() + "," + y.ToString());
        //         if (itemGrid.GetItem(x,y) != null)
        //         {
        //             if (itemGrid.GetItem(x,y) == item)
        //             {
        //                 if (itemObject != itemObject.GetComponent<Item>().item.item3d) 
        //                 {
        //                     itemObject.GetComponent<Item>().item.item3d.SetActive(true);
        //                 }         
        //                 else
        //                 {
        //                     itemObject.SetActive(true);
        //                 }
        //             }
        //             else
        //             {
        //                 itemGrid.GetItem(x,y).item3d.transform.position = itemObject.transform.position;
        //                 itemGrid.GetItem(x,y).item3d.SetActive(true);
        //             }
        //         }
        //         Debug.Log("done checks");
        //     }
        // }
    }

    void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
        // if (itemObject != itemObject.GetComponent<Item>().item.item3d)
        // {
        //     Destroy(itemObject.GetComponent<Item>().item.item3d);
        // }
    }
}
