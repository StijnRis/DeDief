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
        PlaceCeiling();
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
            

            float randomPosition = Random.Range(0, hallLength);
            bool canPlaceCamera = false;

            int tries = 0;
            /*while (!canPlaceCamera)
            {
                randomPosition = Random.Range(0, hallLength);
                canPlaceCamera = hasDoor(randomPosition, sideMultiplier, corridorSize);

                if (tries > 30)
                {
                    Debug.Log("failed");
                    break;
                }
                else
                {
                    tries += 1;
                }
            }*/

            canPlaceCamera = true;

            if (canPlaceCamera)
            {
                GameObject camera = Instantiate(CameraPrefab, transform);

                //Random side of corridor
                float sideMultiplier = 1f;
                if (Random.Range(0, 2) == 1)
                {
                    sideMultiplier *= -1f;
                    camera.transform.localRotation = Quaternion.Euler(0, 90, 0);
                } else
                {
                    camera.transform.localRotation = Quaternion.Euler(0, -90, 0);
                }

                Vector3 position = new Vector3(randomPosition - hallLength / 2, 1f, hallWidth / 2 * sideMultiplier);
                if (rotated)
                {
                    position = new Vector3(hallWidth / 2 * sideMultiplier, 1f, randomPosition - hallLength / 2);
                    
                }
                /*camera.transform.localPosition += position;*/
                camera.transform.Translate(position, transform);
            }
        }
    }

    public bool hasDoor(float position, float side, Vector3 corridorSize)
    {
        Door[] doors = GetComponents<Door>();
        foreach(Door door in doors)
        {
            Vector3 start = door.Start;
            Vector3 end = door.End;
            if (corridorSize.x == 2f * Mathf.Round(door.Start.z * 100f) / 100f)
            {
                Debug.Log("yee");
                start = new Vector3(door.Start.x, 0, door.Start.z);
                end = new Vector3(door.End.x, 0, door.End.z);
            }
            Debug.Log(start.ToString() + "    " + end.ToString());

            if ((start.x > 0 && side > 0) || (start.x < 0 && side < 0)) //Check side
            {
                float startX = door.Start.z;
                float endX = door.End.z;
                if (position >= startX && position <= endX)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
        return true;
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
