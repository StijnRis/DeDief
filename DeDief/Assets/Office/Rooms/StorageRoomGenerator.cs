using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomGenerator : RoomGenerator
{
    public GameObject StorageBox;
    [Range(0.0f, 1.0f)]
    public float fillPercentage = 0.5f;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceBoxesV2();
        PlaceCeiling();
    }
    
    public void PlaceBoxesV2()
    {
        Vector3 roomSize = Box.size - new Vector3(0.2f, 0, 0.2f);

        Vector3 storageBoxSize = new Vector3(0.4f, 0.4f, 0.4f);

        int xSteps = Mathf.FloorToInt(roomSize.x / storageBoxSize.x);
        float xStep = roomSize.x / xSteps;
        int zSteps = Mathf.FloorToInt(roomSize.z / storageBoxSize.z);
        float zStep = roomSize.z / zSteps;
        
        int amountBoxes = 0;
        int amountChecked = 0;
        int totalPossible = xSteps * zSteps;
        float averageAmount = fillPercentage * totalPossible;
        for (float x = -roomSize.x / 2 + xStep / 2; x < roomSize.x / 2; x += xStep)
        {
            for (float z = -roomSize.z / 2 + zStep / 2; z < roomSize.z / 2; z += zStep)
            {
                if (Random.Range(0.0f, (totalPossible - amountChecked)) < (averageAmount - amountBoxes))
                {
                    GameObject storageBox = Instantiate(StorageBox, transform);
                    storageBox.transform.localPosition = new Vector3(x, -roomSize.y / 2 + storageBoxSize.y / 2, z);
                    amountBoxes += 1;
                }
                amountChecked += 1;
            }
        }
    }

    public void PlaceBoxes()
    {
        Vector3 roomSize = GetComponent<BoxCollider>().size;
        float roomSurface = roomSize.x * roomSize.z;

        BoxCollider box = StorageBox.GetComponent<BoxCollider>();
        float boxSurface = box.size.x * roomSize.z;
        float boxAmount = Mathf.Round(roomSurface / boxSurface * 0.5f);

        List<GameObject> walls = GetWalls();
        foreach (GameObject wall in walls)
        {
            Vector3 wallRotation = wall.transform.localRotation.eulerAngles;
            if (wallRotation.y == 90) //Back wall
            {
                PlaceBoxesAtWall(wall.transform, 3);
            }
            if (wallRotation.y == 270) //Front wall
            {
                PlaceBoxesAtWall(wall.transform, 3);
            }
        }
    }

    private void PlaceBoxesAtWall(Transform wall, int amount)
    {
        Vector3 wallSize = wall.GetComponentInParent<BoxCollider>().size;
        float wallLeftPosition = wallSize.z / 2 - wallSize.x / 2;

        for (int i = 0; i < amount; i++)
        {
            GameObject storageBox = Instantiate(StorageBox, transform);
            float storageBoxSize = storageBox.GetComponent<BoxCollider>().size.x;

            float padding = 0.1f;
            float leftPosition = (padding + storageBoxSize) * (i % 2 + 1);
            float topPosition = padding + storageBoxSize;

            Vector3 boxPosition = Vector3.zero;
            if (i == 0)
            {
                boxPosition = new Vector3(leftPosition, 0, topPosition);
            } else if (i == 1)
            {
                boxPosition = new Vector3(leftPosition, 0, topPosition);
            } else if (i == 2)
            {
                boxPosition = new Vector3(leftPosition, 0, topPosition * 2);
            }

            float bottomPosition = -wallSize.y / 2 + storageBoxSize / 2;

            storageBox.transform.localPosition = new Vector3(wallLeftPosition, bottomPosition, wall.localPosition.z);
            storageBox.transform.Translate(-boxPosition, wall);

            float randomRotation = Random.Range(-20, 20); //Degrees
            storageBox.transform.rotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }

    private List<GameObject> GetWalls()
    {
        List<GameObject> walls = new List<GameObject>();

        foreach (Transform child in transform)
        {
            Debug.Log(child);
            GameObject obj = child.gameObject;

            if (obj.tag == "Wall")
            {
                walls.Add(obj);
            }
        }

        return walls;
    }
}
