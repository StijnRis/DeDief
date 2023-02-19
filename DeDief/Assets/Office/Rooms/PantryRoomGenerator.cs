using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryRoomGenerator : RoomGenerator
{
    public GameObject StorageRack;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceRacks();
        /*PlaceCeiling();*/
    }

    private void PlaceRacks()
    {
        BoxCollider roomSize = GetComponent<BoxCollider>();
        float thresholdSideRacks = 1.3f;

        List<GameObject> walls = GetWalls();

        foreach(GameObject wall in walls)
        {
            Vector3 wallRotation = wall.transform.localRotation.eulerAngles;

            if (wallRotation.y == 90) //Back wall
            {
                PlaceRacksOnWall(wall);
            }
            if (wallRotation.y % 180 == 0 && roomSize.size.x >= thresholdSideRacks) //Side walls
            {
                PlaceRacksOnWall(wall);
            }
        }

        

        //Place rack on outer walls
        
    }

    private List<GameObject> GetWalls()
    {
        List<GameObject> walls = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.tag == "Wall")
            {
                walls.Add(child.gameObject);
            }
        }

        return walls;
    }

    private void PlaceRacksOnWall(GameObject wall)
    {
        BoxCollider wallSize = wall.GetComponent<BoxCollider>();
        Vector3 wallPosition = wall.transform.localPosition;

        float wallInnerLength = wallSize.size.z - 2 * wallSize.size.x; //Remove twice the wall thickness to get inner length
        float rackAmount = (int)Mathf.Round(wallInnerLength);

        for (int i = 0; i < rackAmount; i++)
        {
            GameObject rack = Instantiate(StorageRack, transform);

            float rackHeight = 2f;
            float rackDepth = 0.3f;
            float rackWidth = wallInnerLength / rackAmount;
            float bottomPosition = -wallSize.size.y / 2 + rackHeight / 2;

            BoxCollider rackSize = rack.GetComponent<BoxCollider>();
            Vector3 newSize = new Vector3(rackWidth, rackHeight, rackDepth);
            rackSize.size = newSize;

            float mostLeftPosition = -wallInnerLength / 2 + rackWidth * 0.5f;
            rack.transform.localPosition = wallPosition;
            rack.transform.Translate(new Vector3(-rackDepth * 0.5f, bottomPosition, mostLeftPosition + rackWidth * i), wall.transform);

            rack.transform.rotation = wall.transform.rotation * Quaternion.Euler(0, -90, 0);
        }
    }
}
