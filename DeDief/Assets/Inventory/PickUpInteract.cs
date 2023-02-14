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
            Debug.Log(transform.hierarchyCount);
            if (itemGrid.GetItem(0,0) == null && transform.childCount == 0)
            {
                item.pickedUp = false;
                Destroy(gameObject);
                // itemObject.SetActive(false);
            }
        }
    }
}
