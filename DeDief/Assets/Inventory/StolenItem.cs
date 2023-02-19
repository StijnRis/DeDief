using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StolenItem : InventoryItem
{
    public GameObject originItem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("destory item");
            if (grid.gameObject.GetComponent<PickUpInteract>() == null)
            {
                Debug.Log("yes");
                Destroy(originItem);
            }
        }    
    }
}
