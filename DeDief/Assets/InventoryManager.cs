using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotHolder;

    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    public List<ItemClass> items = new List<ItemClass>();

    private GameObject[] slots;

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();

        Add(itemToAdd);
        Remove(itemToRemove);
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemIcon;
                //slots[i].transform.GetChild(0).GetComponent<Text>().text = items[i].ToString();
                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                //slots[i].transform.GetChild(0).GetComponent<Text>().text = items[i].GetQuantity().ToString();
            } 
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                //slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
            
        }
    }

    public void Add(ItemClass item)
    {
        items.Add(item);
        //SlotClass slot = Contains(item);
        //if (slot != null)
        //    slot.AddQuantity(1);
        //else
        //{
        //    items.Add(new SlotClass(item, 1));
        //}
        //RefreshUI();
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item);
        //SlotClass temp = Contains(item);
        //if (temp != null)
        //{
        //    if (temp.GetQuantity() > 1)
        //        temp.SubQuantity(1);
        //    else
        //    {
        //        SlotClass slotToRemove = new SlotClass();
        //        foreach (SlotClass slot in items)
        //        {
        //            if (slot.GetItem() == item)
        //            {
        //                slotToRemove = slot;
        //                break;
        //            }
        //        }
        //        items.Remove(slotToRemove);
        //    }
        //} 
        //else
        //{
        //    return false;
        //}
        //RefreshUI();
        //return true;
    }

    //public SlotClass Contains(ItemClass item)
    //{
    //    foreach(SlotClass slot in items)
    //    {
    //        if (slot.GetItem() == item)
    //        {
    //            return slot;
    //        }
    //    }
    //    return null;
    //}
}
