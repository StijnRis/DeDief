using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRoomGenerator : RoomGenerator
{
    public GameObject Desk;
    public GameObject Chair;

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
        Quaternion forwardsRotation = new Quaternion();
        Quaternion backwardsRotation = new Quaternion();
        forwardsRotation.SetLookRotation(middle);
        middle *= -1;
        backwardsRotation.SetLookRotation(middle);

        Vector2 positionDesk = middle.normalized * (middle.magnitude - 1);
        GameObject desk = Instantiate(Desk, transform);
        desk.transform.localRotation = backwardsRotation;
        desk.transform.localScale = Vector3.one;
        desk.transform.localPosition = new Vector3(positionDesk.x, -Box.size.y / 2, positionDesk.y);
        BoxCollider DeskBox = desk.GetComponent<BoxCollider>();
        DeskBox.size = new Vector3(Mathf.Min(2, Box.size.x - 0.5f), Mathf.Min(1, Box.size.y - 0.5f), Mathf.Min(0.7f, Box.size.z - 0.5f));

        Vector2 positionChair = middle.normalized * (middle.magnitude - 0.5f);
        GameObject chair = Instantiate(Chair, transform);
        chair.transform.localRotation = forwardsRotation;
        chair.transform.localScale = Vector3.one;
        chair.transform.localPosition = new Vector3(positionChair.x, -Box.size.y / 2, positionChair.y);
        BoxCollider ChairBox = desk.GetComponent<BoxCollider>();
        ChairBox.size = new Vector3(Mathf.Min(0.5f, Box.size.x), Mathf.Min(0.5f, Box.size.y), Mathf.Min(0.5f, Box.size.z));
    }
}
