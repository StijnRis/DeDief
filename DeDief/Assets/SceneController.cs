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

            PlacePlayer();

            SetTargetAgents(player);
        }
    }

    void SpawnAgents()
    {        
        for (int i = 0; i < AgentCount; i++)
        {
            GameObject randomFloor = getRandomFloor();
            GameObject agent = Instantiate(agentPrefab, randomFloor.transform.position, Quaternion.identity);

            allAgents.Add(agent);
        }

    }

    void PlacePlayer()
    {
        GameObject randomFloor = getRandomFloor();

        player.transform.position = randomFloor.transform.position;
    }

    void SetTargetAgents(GameObject target)
    {
        for (int i = 0; i < allAgents.Count; i++)
        {
            allAgents[i].GetComponent<AgentController>().setTarget(player);
        }
    }

    GameObject getRandomFloor()
    {
        GameObject[] allFloors = GameObject.FindGameObjectsWithTag("Floor");
        GameObject randomFloor = allFloors[Random.Range(0, allFloors.Length - 1)];
        return randomFloor;
    }
}
