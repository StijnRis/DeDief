using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract partial class RoomGenerator : MonoBehaviour
{
    protected BoxCollider Box;
    public GameObject Wall;
    public GameObject Floor;
    public GameObject Light;
    public float value;
    public float valueRank = 1;
    protected Door[] Doors;

    private int wallIndex = 0;
    private float wallThickness = 0.05f;
    protected List<GameObject> Walls;

    public void Start()
    {
        Box = GetComponent<BoxCollider>();
        Doors = GetComponents<Door>();
        Generate();
    }

    public abstract void Generate();

    protected void PlaceWalls()
    {
        Walls = new List<GameObject>();

        List<Vector3> points = new List<Vector3>();
        List<Vector3> noConnectPoints = new List<Vector3>();
        points.Add(new Vector3(0.5f * Box.size.x, 0, 0.5f * Box.size.z) + new Vector3(-wallThickness / 2, 0, -wallThickness / 2));
        points.Add(new Vector3(0.5f * Box.size.x, 0, -0.5f * Box.size.z) + new Vector3(-wallThickness / 2, 0, wallThickness / 2));
        points.Add(new Vector3(-0.5f * Box.size.x, 0, -0.5f * Box.size.z) + new Vector3(wallThickness / 2, 0, wallThickness / 2));
        points.Add(new Vector3(-0.5f * Box.size.x, 0, 0.5f * Box.size.z) + new Vector3(wallThickness / 2, 0, -wallThickness / 2));
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
        floor.transform.localPosition = new Vector3(0, - Box.size.y / 2 - floorDepth / 2, 0);
        floor.layer = LayerMask.NameToLayer("Floor");
    }

    protected void PlaceCeiling()
    {
        if (!GetComponentInParent<OfficeGenerator>().Ceiling)
        {
            return;
        }
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.transform.SetParent(transform);
        ceiling.transform.localScale = new Vector3(Box.size.x, wallThickness, Box.size.z);
        ceiling.transform.position = new Vector3(transform.position.x, transform.position.y + Box.size.y / 2 - wallThickness, transform.position.z);
        ceiling.transform.rotation = transform.rotation;
        ceiling.name = "Ceiling";
        ceiling.layer = LayerMask.NameToLayer("Ceiling");

        GameObject light = Instantiate(Light, transform);
        light.transform.localPosition = new Vector3(0, Box.size.y / 2 - wallThickness * 2, 0);
        light.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    protected void CreateWall(Vector3 startCorner, Vector3 endCorner)
    {
        GameObject wall = Instantiate(Wall, transform);
        wall.name = "Wall" + wallIndex.ToString();
        wall.transform.SetParent(transform);

        Vector3 rotation = (endCorner - startCorner).normalized;
        float distance = Vector3.Distance(startCorner, endCorner);
        BoxCollider WallBox = wall.GetComponent<BoxCollider>();
        Vector3 size = new Vector3(wallThickness, Box.size.y - wallThickness, distance);
        if (rotation.x > 0.5 || rotation.x < -0.5)
        {
            size.z += wallThickness;
        } else
        {
            size.z -= wallThickness;
        }
        WallBox.size = size;
        wall.transform.localPosition = startCorner + distance / 2 * rotation - new Vector3(0, wallThickness, 0);
        Quaternion quaternion = Quaternion.LookRotation(rotation, Vector3.up);
        wall.transform.localRotation = quaternion;

        Walls.Add(wall);
    }

    protected GameObject GetWall(float rotation)
    {
        foreach (GameObject wall in Walls)
        {
            Vector3 wallRotation = wall.transform.localRotation.eulerAngles;
            if (wallRotation.y == rotation)
            {
                return wall;
            }
        }
        return null;
    }

    public void OnDestroy()
    {
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
    }
}
