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
        PlaceDesk();
    }

    private void PlaceDesk()
    {
        Vector2 middle = (Doors[0].Start + Doors[0].End) / 2;
        middle *= -1;
        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(middle);

        Vector2 positionDesk = middle.normalized * (middle.magnitude - 1);
        GameObject desk = GameObject.CreatePrimitive(PrimitiveType.Cube);
        desk.transform.parent = transform;
        desk.transform.localRotation = rotation;
        desk.transform.localScale = new Vector3(2, 1, 0.7f);
        desk.transform.localPosition = new Vector3(positionDesk.x, 0, positionDesk.y);

        Vector2 positionChair = middle.normalized * (middle.magnitude - 0.5f);
        GameObject chair = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chair.transform.parent = transform;
        desk.transform.localRotation = rotation;
        chair.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        chair.transform.localPosition = new Vector3(positionChair.x, 0, positionChair.y);
    }
}
