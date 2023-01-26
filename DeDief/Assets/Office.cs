using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{
    List<GameObject> rooms;

    public void setup()
    {
        this.rooms = new List<GameObject>();
    }

    public void AddRoom(Area arae)
    {
        GameObject room = new GameObject("Room");
        room.transform.SetParent(this.transform);
        OfficeRoom roomScript = room.gameObject.AddComponent<OfficeRoom>();
        roomScript.setArea(arae);
        roomScript.SurroundWithWall();
        this.rooms.Add(room);
    }

    public void OnDestroy()
    {
        foreach (GameObject room in this.rooms){
            Destroy(room);
        }
    }
}
