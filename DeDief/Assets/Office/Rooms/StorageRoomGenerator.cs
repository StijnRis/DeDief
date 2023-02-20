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
}
