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
        /*PlaceCeiling();*/
    }

    private void PlaceDesk()
    {
        GameObject desk = Instantiate(ConsulattionStation, transform);

        Quaternion rotation = new Quaternion();
        Vector3 maxSize = new Vector3(Box.size.x - 1f, Box.size.y, Box.size.z - 1f);
        if (Box.size.x > Box.size.z)
        {
            rotation.SetLookRotation(new Vector3(1, 0, 0));
            maxSize.x -= 0.5f;
        } else
        {
            rotation.SetLookRotation(new Vector3(0, 0, 1));
            maxSize.z -= 0.5f;
        }
        maxSize = rotation * maxSize;
        maxSize = new Vector3(Mathf.Abs(maxSize.x), Mathf.Abs(maxSize.y), Mathf.Abs(maxSize.z));

        desk.transform.localRotation = rotation;
        desk.transform.localPosition = new Vector3(0, 0, 0);

        BoxCollider DeskSize = desk.GetComponent<BoxCollider>();
        DeskSize.size = maxSize;
    }
}
