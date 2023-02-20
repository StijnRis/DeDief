using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator : RoomGenerator
{
    public GameObject Door;

    public override void Generate()
    {
        OnDestroy();
        CreateDoors();
        PlaceFloor();
        /*PlaceCeiling();*/
        PlaceCameras();
    }

    public void CreateDoors()
    {
        foreach (Door door in Doors)
        {
            CreateDoor(door.Start, door.End);
        }
    }

    public  void CreateDoor(Vector3 startCorner, Vector3 endCorner)
    {
        float thickness = 0.05f;
        GameObject door = Instantiate(Door, transform);

        Vector3 rotation = (endCorner - startCorner).normalized;
        float distance = Vector3.Distance(startCorner, endCorner);
        BoxCollider WallBox = door.GetComponent<BoxCollider>();
        WallBox.size = new Vector3(thickness, Box.size.y, distance + thickness);
        door.transform.localPosition = startCorner + distance / 2 * rotation;
        Quaternion quaternion = Quaternion.LookRotation(rotation, Vector3.up);
        door.transform.localRotation = quaternion;
    }


    public void PlaceCameras()
    {
        GameObject CameraPrefab = GetComponentInParent<OfficeGenerator>().Camera;

        BoxCollider box = GetComponent<BoxCollider>();
        string longestAxis = getLongestAxis(box);

        Vector3 corridorSize = box.size;
        bool rotated = true;
        if (longestAxis == "x")
        {
            rotated = true;
            corridorSize = new Vector3(box.size.z, box.size.y, box.size.x);
        }

        float hallLength = corridorSize.z;
        float hallWidth = corridorSize.x;

        float corridorSurface = corridorSize.x * corridorSize.z;
        int cameraAmount = Mathf.RoundToInt(corridorSurface / 20f);

        for (int i = 0; i < cameraAmount; i++)
        {
            //Random position for camera
            float randomPositionX = Random.Range(0.2f, hallLength);
            float randomPositionZ = Random.Range(0, hallWidth / 2 - 0.1f);

            GameObject camera = Instantiate(CameraPrefab, transform);

            //Random side of corridor
            float sideMultiplier = 1f;
            if (Random.Range(0, 2) == 1)
            {
                sideMultiplier *= -1f;
            } else
            {
                camera.transform.localRotation = Quaternion.Euler(0, -180, 0);
            }

            Vector3 position = new Vector3(randomPositionX - hallLength / 2, box.size.y / 2 - 0.1f, randomPositionZ * sideMultiplier);
            if (rotated)
            {
                position = new Vector3(randomPositionZ * sideMultiplier, box.size.y / 2 - 0.1f, randomPositionX - hallLength / 2);
                    
            }
            camera.transform.Translate(position, transform);
        }
    }

    public string getLongestAxis(BoxCollider box)
    {
        if (box.size.x > box.size.z)
        {
            return "x";
        } else
        {
            return "z";
        }
    }
}
