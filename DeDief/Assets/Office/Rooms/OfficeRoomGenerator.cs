using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRoomGenerator : RoomGenerator
{
    public GameObject WorkStation;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceDesk();
        /*PlaceCeiling();*/
    }

    private void PlaceDesk()
    {
        GameObject workStation = Instantiate(WorkStation, transform);
        BoxCollider workStationBox = workStation.GetComponent<BoxCollider>();
        Vector3 size = new Vector3(Mathf.Min(2, Box.size.x - 0.5f), Mathf.Min(1, Box.size.y - 0.5f), Mathf.Min(1.4f, Box.size.z - 0.5f));
        workStationBox.size = size;
        workStation.transform.localPosition = new Vector3(0, -Box.size.y / 2 + workStationBox.size.y / 2, - Box.size.z / 2.2f + size.z / 2);
    }
}
