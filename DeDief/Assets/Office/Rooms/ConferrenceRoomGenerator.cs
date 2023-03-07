using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferrenceRoomGenerator : RoomGenerator
{
    public GameObject ConsulattionStation;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceDesk();
        PlaceCeiling();
        GetWall(0).GetComponent<WallGenerator>().placePainting();
    }

    private void PlaceDesk()
    {
        GameObject desk = Instantiate(ConsulattionStation, transform);

        Vector3 maxSize = new Vector3(Box.size.x - 1f, Box.size.y, Box.size.z - 2f);

        desk.transform.localPosition = new Vector3(0, 0, 0);

        BoxCollider DeskSize = desk.GetComponent<BoxCollider>();
        DeskSize.size = maxSize;

        desk.AddComponent<Value>().value = value;
    }
}
