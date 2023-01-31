using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRoomGenerator : RoomGenerator
{
    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
    }
}
