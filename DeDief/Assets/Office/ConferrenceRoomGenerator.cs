using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferrenceRoomGenerator : RoomGenerator
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
        Quaternion rotation = new Quaternion();
        if (Box.size.x > Box.size.z)
        {
            rotation.SetLookRotation(new Vector3(1, 0, 0));
        } else
        {
            rotation.SetLookRotation(new Vector3(0, 0, 1));
        }

        GameObject desk = Instantiate(Desk, transform);
        desk.transform.localRotation = rotation;
        desk.transform.localScale = Vector3.one;
        desk.transform.localPosition = new Vector3(0, -Box.size.y / 2, 0);
        BoxCollider DeskSize = desk.GetComponent<BoxCollider>();
        DeskSize.size = new Vector3(Mathf.Min(2, Box.size.x - 0.5f), Mathf.Min(1, Box.size.y - 0.5f), Mathf.Min(0.7f, Box.size.z - 0.5f));

        /*Vector2 positionChair = middle.normalized * (middle.magnitude - 0.5f);
        GameObject chair = Instantiate(Chair, transform);
        chair.transform.localRotation = forwardsRotation;
        chair.transform.localScale = Vector3.one;
        chair.transform.localPosition = new Vector3(positionChair.x, -Size.size.y / 2, positionChair.y);
        Size ChairSize = desk.GetComponent<Size>();
        ChairSize.size = new Vector3(Mathf.Min(0.5f, Size.size.x), Mathf.Min(0.5f, Size.size.y), Mathf.Min(0.5f, Size.size.z));*/
    }
}
