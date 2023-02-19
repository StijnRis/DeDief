using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class RoomGenerator : MonoBehaviour
{
    protected BoxCollider Box;
    public GameObject Wall;
    public GameObject Floor;
    public GameObject Light;
    protected Door[] Doors;

    private int wallIndex = 0;

    public void Start()
    {
        Box = GetComponent<BoxCollider>();
        Doors = GetComponents<Door>();
        Generate();
    }

    public abstract void Generate();

    protected void PlaceWalls()
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> noConnectPoints = new List<Vector3>();
        points.Add(new Vector3((float)(0.5 * Box.size.x), 0, (float)(0.5 * Box.size.z)));
        points.Add(new Vector3((float)(0.5 * Box.size.x), 0, (float)(-0.5 * Box.size.z)));
        points.Add(new Vector3((float)(-0.5 * Box.size.x), 0, (float)(-0.5 * Box.size.z)));
        points.Add(new Vector3((float)(-0.5 * Box.size.x), 0, (float)(0.5 * Box.size.z)));
        foreach (Door door in Doors)
        {
            points.Add(door.Start);
            noConnectPoints.Add(door.Start);
            points.Add(door.End);
            noConnectPoints.Add(door.End);
        }

        List<Vector3> sortedPoints = points.OrderBy(o => Vector3.SignedAngle(o, Vector3.forward, Vector3.up)).ToList();

        Vector3 previous = sortedPoints.Last();

        wallIndex = 0;
        for (int i = 0; i < sortedPoints.Count; i++)
        {
            if (!(noConnectPoints.Contains(previous) && noConnectPoints.Contains(sortedPoints[i])))
            {
                CreateWall(previous, sortedPoints[i]);
                wallIndex += 1;
            }
            previous = sortedPoints[i];
        }
    }

    protected void PlaceFloor()
    {
        float floorDepth = 0.1f;
        GameObject floor = Instantiate(Floor, transform);
        floor.transform.localScale = new Vector3(Box.size.x, floorDepth, Box.size.z);
        floor.transform.position = new Vector3(transform.position.x, transform.position.y - Box.size.y / 2 - floorDepth / 2, transform.position.z);
        floor.layer = LayerMask.NameToLayer("Floor");
    }

    protected void PlaceCeiling()
    {
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.transform.SetParent(transform);
        ceiling.transform.localScale = new Vector3(Box.size.x, 0.1f, Box.size.z);
        ceiling.transform.position = new Vector3(transform.position.x, transform.position.y + Box.size.y / 2, transform.position.z);
        ceiling.transform.rotation = transform.rotation;
        ceiling.name = "Ceiling";


        float ceilingSurface = ceiling.transform.localScale.x * ceiling.transform.localScale.z;
        float lightCoverage = 10f;
        int lightAmount = Mathf.FloorToInt(ceilingSurface / lightCoverage);

        for (int i = 0; i < lightAmount; i++)
        {
            GameObject light = Instantiate(Light, transform);
            light.transform.localPosition = new Vector3(0, Box.size.y / 2 - ceiling.transform.localScale.y, 0);
            light.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        
    }

    protected void CreateWall(Vector3 startCorner, Vector3 endCorner)
    {
        float thickness = 0.1f;
        GameObject wall = Instantiate(Wall, transform);
        wall.name = "Wall" + wallIndex.ToString();
        wall.transform.SetParent(transform);

        Vector3 rotation = (endCorner - startCorner).normalized;
        float distance = Vector3.Distance(startCorner, endCorner);
        BoxCollider WallBox = wall.GetComponent<BoxCollider>();
        WallBox.size = new Vector3(thickness, Box.size.y, distance + thickness);
        wall.transform.localPosition = startCorner + distance / 2 * rotation;
        Quaternion quaternion = Quaternion.LookRotation(rotation, Vector3.up);
        wall.transform.localRotation = quaternion;
    }

    public void OnDestroy()
    {
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
    }
}
