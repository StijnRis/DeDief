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
        Vector2 middle2D = (Doors[0].Start + Doors[0].End) / 2;
        Vector3 middle = new Vector3(middle2D.x, 0, middle2D.y);
        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(middle);

        GameObject workStation = Instantiate(WorkStation, transform);
        BoxCollider workStationBox = workStation.GetComponent<BoxCollider>();
        Vector3 size = rotation * new Vector3(Mathf.Min(2, Box.size.x - 0.5f), Mathf.Min(1, Box.size.y - 0.5f), Mathf.Min(1.4f, Box.size.z - 0.5f));
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
        workStationBox.size = size;
        workStation.transform.localRotation = rotation;
        workStation.transform.localScale = Vector3.one;
        workStation.transform.localPosition = new Vector3(0, -Box.size.y / 2 + workStationBox.size.y / 2, 0);
    }
}
