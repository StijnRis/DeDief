using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomGenerator : RoomGenerator
{
    public GameObject StorageRack;

    public override void Generate()
    {
        OnDestroy();
        PlaceWalls();
        PlaceFloor();
        PlaceRack();
        /*PlaceCeiling();*/
    }

    private void PlaceRack()
    {
        GameObject rack = Instantiate(StorageRack, transform);

        BoxCollider rackSize = rack.GetComponent<BoxCollider>();

        //Place rack on outer walls
        
    }
}
