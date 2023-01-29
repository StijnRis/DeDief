using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class RoomGenerator : MonoBehaviour
{
    private Size Size;
    private Door Door;

    public void Start()
    {
        Size = GetComponent<Size>();
        Door = GetComponent<Door>();
        Generate();
    }

    public abstract void Generate();


    protected void PlaceWalls()
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(new Vector3((float)(0.5 * Size.size.x), 0, (float)(0.5 * Size.size.z)));
        points.Add(new Vector3((float)(0.5 * Size.size.x), 0, (float)(-0.5 * Size.size.z)));
        points.Add(new Vector3((float)(-0.5 * Size.size.x), 0, (float)(-0.5 * Size.size.z)));
        points.Add(new Vector3((float)(-0.5 * Size.size.x), 0, (float)(0.5 * Size.size.z)));
        Vector3? door1 = null;
        Vector3? door2 = null;
        if (Door != null)
        {
            door1 = new Vector3(Door.Start.x, 0, Door.Start.y);
            points.Add((Vector3)door1);
            door2 = new Vector3(Door.End.x, 0, Door.End.y);
            points.Add((Vector3)door2);
        }
        List<Vector3> sortedPoints = points.OrderBy(o => Vector3.SignedAngle(o, Vector3.forward, Vector3.up)).ToList();

        Vector3 previous = sortedPoints.Last();
        for (int i = 0; i < sortedPoints.Count; i++)
        {
            if (!((previous == door1 || previous == door2) && (sortedPoints[i] == door1 || sortedPoints[i] == door2)))
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
    }

    protected void CreateWall(Vector3 startCorner, Vector3 endCorner)
    {
        GameObject startObj = new GameObject();
        startObj.transform.SetParent(transform);
        startObj.transform.localPosition = startCorner;
        startObj.name = "Pillar";

        GameObject endObj = new GameObject();
        endObj.transform.SetParent(transform);
        endObj.transform.localPosition = endCorner;
        endObj.name = "Pillar";

        endObj.transform.LookAt(startObj.transform.position);
        startObj.transform.LookAt(endObj.transform.position);

        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(transform);
        float distance = Vector3.Distance(startCorner, endCorner);
        wall.name = "Wall";
        wall.transform.localScale = new Vector3(0.1f, Size.size.y, distance);
        wall.transform.position = startObj.transform.position + distance / 2 * startObj.transform.forward;
        wall.transform.rotation = startObj.transform.rotation;
    }

    public void OnDestroy()
    {
        foreach (Transform c in this.transform)
        {
            Destroy(c.gameObject);
        }
    }
}
