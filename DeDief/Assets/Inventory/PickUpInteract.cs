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
            if (itemGrid.GetItem(0,0) == null && transform.childCount == 0)
            {
                if (itemObject != itemObject.GetComponent<Item>().item.item3d)
                    Destroy(itemObject);
                Destroy(gameObject);
                // itemObject.SetActive(false);
            }
        }
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
