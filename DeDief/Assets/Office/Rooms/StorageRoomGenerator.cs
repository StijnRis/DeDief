using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomGenerator : RoomGenerator
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
        List<GameObject> walls = GetWalls();

        foreach(GameObject wall in walls)
        {
            if (wall.transform.rotation.y == 0) //Back wall
            {
                BoxCollider wallSize = wall.GetComponent<BoxCollider>();
                Vector3 wallPosition = wall.transform.localPosition;
                Quaternion wallRotation = wall.transform.rotation;

                float wallLength = wallSize.size.z;

                float rackAmount = (int) Mathf.Round(wallLength);

                for (int i = 0; i < rackAmount; i++)
                {
                    GameObject rack = Instantiate(StorageRack, transform);

                    float rackHeight = 2f;
                    float rackDepth = 0.3f;
                    float rackWidth = wallLength / rackAmount;
                    float bottomPosition = wallSize.size.y / 2 - rackHeight / 2;

                    BoxCollider rackSize = rack.GetComponent<BoxCollider>();
                    Vector3 newSize = new Vector3(rackDepth, rackHeight, rackWidth);
                    rackSize.size = newSize;

                    float mostLeftPosition = -wallLength / 2 + rackWidth * 0.5f;
                    rack.transform.localPosition = new Vector3(wallPosition.x - rackDepth * 0.5f, -bottomPosition, mostLeftPosition + rackWidth * i);
                    rack.transform.rotation = wallRotation;
                }
            }
            if (Mathf.Abs(wall.transform.rotation.y) == 90) //Side walls
            {

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
}
