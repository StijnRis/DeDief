using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    // [HideInInspector]
    public GameObject agent;
    private bool checkPickedUp = false;
    
    protected override void Interact()
    {
        if (agent != null)
        {
            if (agent.GetComponent<AgentController>() != null)
            {
                agent.GetComponent<AgentController>().weapon = null;
            }
        }
        gameObject.SetActive(false);
        base.Interact();
        checkPickedUp = true;
    }

    void Update()
    {
        if (checkPickedUp)
        {
            if (item.pickedUp == PickUpMode.InPlayerInventory)
            {
                agent = null;
                transform.SetParent(GameObject.FindGameObjectWithTag("Office").transform);
                checkPickedUp = false;
            }
        }
    }

    public void ResetAgent()
    {
        agent.GetComponent<AgentController>().weapon = gameObject;
    }
}
