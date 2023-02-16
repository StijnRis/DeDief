using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomGenerator : RoomGenerator
{
    public GameObject StorageBox;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceBoxes();
        /*PlaceCeiling();*/
    }

    public void PlaceBoxes()
    {
        Vector3 roomSize = GetComponent<BoxCollider>().size;
        float roomSurface = roomSize.x * roomSize.z;

        BoxCollider box = StorageBox.GetComponent<BoxCollider>();
        float boxSurface = box.size.x * roomSize.z;
        float boxAmount = Mathf.Round(roomSurface / boxSurface * 0.5f);

        List<float> positions = new List<float>();
        for (int i = 0; i < boxAmount; i++) {
            GameObject storageBox = Instantiate(StorageBox, transform);

            float randomPosition = getPossiblePosition(positions, roomSize.x, box.size.x);
            positions.Add(randomPosition);

            storageBox.transform.localPosition = new Vector3(randomPosition, 0, 0);

            float randomRotation = Random.Range(0, 360);
            storageBox.transform.rotation = Quaternion.Euler(new Vector3(0, randomRotation, 0));
        }
    }

    public float getPossiblePosition(List<float> positions, float roomSize, float boxSize)
    {
        int endlessLoopLimit = 200;
        int i = 0;

        float randomPosition = 0;

        bool positionFound = false;
        while (!positionFound)
        {
            randomPosition = Random.Range(-roomSize / 2, roomSize / 2);
            /*Debug.Log("new random position: " + randomPosition);*/

            if (positions.Count > 0)
            {
                foreach (float position in positions)
                {
                    float margin = Mathf.Abs(position - randomPosition);
                    /*Debug.Log("margin: " + margin);*/
                    if (margin >= boxSize * 2)
                    {
                        positionFound = positionFound && true;
                    }
                    else
                    {
                        positionFound = false;
                    }
                }
            }
            else
            {
                break;
            }

            if (i > endlessLoopLimit)
            {
                Debug.Log("nothing found");
                break;
            }
            i += 1;
        }

        Debug.Log("using: " + randomPosition);

        return randomPosition;
    }
}
