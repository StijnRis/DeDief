using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomGenerator : MonoBehaviour
{
    public Size Size;

    public void Start()
    {
        Size = GetComponent<Size>();
        Generate();
    }

    public abstract void Generate();

 
    protected void SurroundWithWall()
    {
        Vector2 one = new Vector2((float)(0.5 * Size.size.x), (float)(0.5 * Size.size.z));
        Vector2 two = new Vector2((float)(0.5 * Size.size.x), (float)(-0.5 * Size.size.z));
        Vector2 three = new Vector2((float)(-0.5 * Size.size.x), (float)(-0.5 * Size.size.z));
        Vector2 four = new Vector2((float)(-0.5 * Size.size.x), (float)(0.5 * Size.size.z));
        CreateWall(one, two);
        CreateWall(two, three);
        CreateWall(three, four);
        CreateWall(four, one);
    }

    protected void placeFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.SetParent(transform);
        floor.transform.localScale = new Vector3(Size.size.x, 0.1f, Size.size.z);
        floor.transform.position = new Vector3(transform.position.x, transform.position.y - Size.size.y / 2, transform.position.z);
    }

    protected void CreateWall(Vector2 startCorner, Vector2 endCorner)
    {
        Vector3 start = new Vector3(startCorner.x, 0, startCorner.y);
        GameObject startObj = new GameObject();
        startObj.transform.SetParent(transform);
        startObj.transform.localPosition = start;
        startObj.name = "Pillar";

        Vector3 end = new Vector3(endCorner.x, 0, endCorner.y);
        GameObject endObj = new GameObject();
        endObj.transform.SetParent(transform);
        endObj.transform.localPosition = end;
        endObj.name = "Pillar";

        endObj.transform.LookAt(startObj.transform.position);
        startObj.transform.LookAt(endObj.transform.position);

        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(transform);
        float distance = Vector3.Distance(start, end);
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
