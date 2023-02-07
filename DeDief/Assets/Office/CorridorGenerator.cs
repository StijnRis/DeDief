using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator : RoomGenerator
{
    public override void Generate()
    {
        OnDestroy();
        PlaceFloor();
        PlaceCeiling();
    }
}
