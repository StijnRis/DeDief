using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpyRoomGenerator : RoomGenerator
{
    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        /*PlaceCeiling();*/
    }
}
