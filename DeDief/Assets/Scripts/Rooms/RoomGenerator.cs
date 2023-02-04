using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class RoomGenerator : MonoBehaviour
{
    protected Size Size;
    protected Door[] Doors;

    public void Start()
    {
        Size = GetComponent<Size>();
        Doors = GetComponents<Door>();
        Generate();
    }

    public abstract void Generate();


    protected void PlaceWalls()
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> noConnectPoints = new List<Vector3>();
        points.Add(new Vector3((float)(0.5 * Size.size.x), 0, (float)(0.5 * Size.size.z)));
        points.Add(new Vector3((float)(0.5 * Size.size.x), 0, (float)(-0.5 * Size.size.z)));
        points.Add(new Vector3((float)(-0.5 * Size.size.x), 0, (float)(-0.5 * Size.size.z)));
        points.Add(new Vector3((float)(-0.5 * Size.size.x), 0, (float)(0.5 * Size.size.z)));
        foreach (Door door in Doors)
        {
            Vector3 door1 = new Vector3(door.Start.x, 0, door.Start.y);
            points.Add(door1);
            noConnectPoints.Add(door1);
            Vector3 door2 = new Vector3(door.End.x, 0, door.End.y);
            points.Add(door2);
            noConnectPoints.Add(door2);
        }

        List<Vector3> sortedPoints = points.OrderBy(o => Vector3.SignedAngle(o, Vector3.forward, Vector3.up)).ToList();

        Vector3 previous = sortedPoints.Last();
        for (int i = 0; i < sortedPoints.Count; i++)
        {
            if (!(noConnectPoints.Contains(previous) && noConnectPoints.Contains(sortedPoints[i])))
            {
                CreateWall(previous, sortedPoints[i]);
            }
            previous = sortedPoints[i];
        }
    }

    protected void PlaceFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.SetParent(transform);
        floor.transform.localScale = new Vector3(Size.size.x, 0.1f, Size.size.z);
        floor.transform.position = new Vector3(transform.position.x, transform.position.y - Size.size.y / 2, transform.position.z);
        floor.layer = LayerMask.NameToLayer("Floor");
        floor.tag = "Floor";
    }

    protected void CreateWall(Vector3 startCorner, Vector3 endCorner)
    {
        float thickness = 0.1f;

        Vector3 rotation = (endCorner - startCorner).normalized;

        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(transform);
        float distance = Vector3.Distance(startCorner, endCorner);
        wall.name = "Wall";
        wall.transform.localScale = new Vector3(thickness, Size.size.y, distance + thickness);
        wall.transform.localPosition = startCorner + distance / 2 * rotation;
        Quaternion quaternion = Quaternion.LookRotation(rotation, Vector3.up);
        wall.transform.localRotation = quaternion;
        wall.layer = LayerMask.NameToLayer("Walls");
        wall.tag = "Wall";

    }

    public void OnDestroy()
    {
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
    }
}
