using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : Weapon
{
    // [HideInInspector]
    public GameObject agent;

    public GameObject stolenPrefab;

    private bool checkPickedUp = false;

    protected override void Interact()
    {
        item3d = Instantiate(stolenPrefab) as GameObject;
        item3d.transform.SetParent(GameObject.FindGameObjectWithTag("Office").transform);
        base.Interact();
    }

    public void OnDestroy()
    {
        // agent = null;
        agent.GetComponent<AgentController>().weapon = null;
    }
}
