using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRoomGenerator : RoomGenerator
{
    public GameObject WorkStation;
    public GameObject Plant;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceDesk();
        placePlants();
        PlaceCeiling();
        GetBackWall().GetComponent<WallGenerator>().placePainting();
    }

    private void PlaceDesk()
    {
        GameObject workStation = Instantiate(WorkStation, transform);
        BoxCollider workStationBox = workStation.GetComponent<BoxCollider>();
        Vector3 size = new Vector3(Mathf.Min(2, Box.size.x - 0.5f), Mathf.Min(1, Box.size.y - 0.5f), Mathf.Min(1.4f, Box.size.z - 0.5f));
        workStationBox.size = size;
        workStation.transform.localPosition = new Vector3(0, -Box.size.y / 2 + workStationBox.size.y / 2, - Box.size.z / 2.2f + size.z / 2);
    }

    private void placePlants()
    {
        Vector3 size = new Vector3(0.5f, 2f, 0.5f);
        if (size.x < (Box.size.x / 2 - 0.5f) && size.z < (Box.size.z / 2 - 0.5f))
        {
            if (Random.Range(0, 2f) < 1)
            {
                GameObject plant1 = Instantiate(Plant, transform);
                BoxCollider plant1Box = plant1.GetComponent<BoxCollider>();
                plant1Box.size = size;
                plant1.transform.localPosition = new Vector3(-Box.size.x / 2 + size.x, -Box.size.y / 2 + plant1Box.size.y / 2, Box.size.z / 2.2f - size.z / 2);
            }

            if (Random.Range(0, 2f) < 1)
            {
                GameObject plant2 = Instantiate(Plant, transform);
                BoxCollider plant2Box = plant2.GetComponent<BoxCollider>();
                plant2Box.size = size;
                plant2.transform.localPosition = new Vector3(Box.size.x / 2 - size.x, -Box.size.y / 2 + plant2Box.size.y / 2, Box.size.z / 2.2f - size.z / 2);
            }
        }
        
    }
}
