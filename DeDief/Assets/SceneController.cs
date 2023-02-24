using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Agents")]
    public int AgentCount = 5;
    public GameObject agentPrefab;

    private List<GameObject> allAgents = new List<GameObject>();
    private List<GameObject> corridorFloors = new List<GameObject>();

    private bool startGame = true;

    bool followPlayer = false;

    public void StartGame()
    {
        GetAllCorridorFloors();
        SpawnAgents();

        /*PlacePlayer();*/
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame) //Run if all of office is loaded
        {
            StartGame();
            startGame = !startGame;
        }
    }

    void SpawnAgents()
    {        
        for (int i = 0; i < AgentCount; i++)
        {
            GameObject randomFloor = getRandomCorridorFloor();
            GameObject agent = Instantiate(agentPrefab, randomFloor.transform.position, Quaternion.identity);

            //Create waypoints
            for (int x = 0; x < 3; x++)
            {
                randomFloor = getRandomCorridorFloor();
                agent.AddComponent<Waypoint>().location = randomFloor.transform.position;
            }

            //Set first waypoint for agent
            agent.GetComponent<AgentController>().NextWaypoint();

            allAgents.Add(agent);
        }
    }

    public void SendNearestAgentToPlayer()
    {
        SendNearestAgentTo(player.transform.position);
    }

    public void SendNearestAgentTo(Vector3 position)
    {
        float shortestDistance = int.MaxValue;
        if (allAgents.Count > 0)
        {
            GameObject nearestAgent = allAgents[0];
            foreach (GameObject agent in allAgents)
            {
                float distance = Vector3.Distance(agent.transform.position, player.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestAgent = agent;
                }
            }
            nearestAgent.GetComponent<AgentController>().currentWaypoint = player.transform.position;
        }
    }


    /*void PlacePlayer()
    {
        GameObject randomFloor = getRandomFloor();

        player.transform.position = randomFloor.transform.position + new Vector3(0,1,0);
    }*/


    //Floors
    private void GetAllCorridorFloors()
    {
        corridorFloors = new List<GameObject>();
        GameObject[] allFloors = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floor in allFloors)
        {
            GameObject parent = floor.transform.parent.gameObject;
            /*Debug.Log(parent.name);*/
            if (parent.GetComponent<CorridorGenerator>() != null)
            {

                corridorFloors.Add(floor);
            }
        }
    }

    GameObject getRandomFloor()
    {
        GameObject[] allFloors = GameObject.FindGameObjectsWithTag("Floor");
        GameObject randomFloor = allFloors[Random.Range(0, allFloors.Length - 1)];
        return randomFloor;
    }

    GameObject getRandomCorridorFloor()
    {
        GameObject randomFloor = corridorFloors[Random.Range(0, corridorFloors.Count - 1)];
        return randomFloor;
    }
}
