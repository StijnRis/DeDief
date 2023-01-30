using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{
    public int AgentCount = 5;
    public GameObject agentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Spawned");
            SpawnAgents();
        }
    }

    void SpawnAgents()
    {
        GameObject[] allFloors = GameObject.FindGameObjectsWithTag("Floor");

        GameObject holyGrail = allFloors[Random.Range(0, allFloors.Length - 1)];
        
        for (int i = 0; i < AgentCount; i++)
        {
            int randomIndex = Random.Range(0, allFloors.Length - 1);
            GameObject randomFloor = allFloors[randomIndex];
            GameObject agent = Instantiate(agentPrefab, randomFloor.transform.position, Quaternion.identity);
            agent.GetComponent<AgentController>().target = holyGrail;
        }

        

    }
}
