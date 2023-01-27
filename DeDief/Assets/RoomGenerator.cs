using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public void Start()
    {
        Generate();
    }


    public void Generate()
    {
        OnDestroy();
        SurroundWithWall();
    }

    private void SurroundWithWall()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector2 one = new Vector2((float)(0.5 * collider.size.x), (float)(0.5 * collider.size.z));
        Vector2 two = new Vector2((float)(0.5 * collider.size.x), (float)(-0.5 * collider.size.z));
        Vector2 three = new Vector2((float)(-0.5 * collider.size.x), (float)(-0.5 * collider.size.z));
        Vector2 four = new Vector2((float)(-0.5 * collider.size.x), (float)(0.5 * collider.size.z));
        CreateWall(one, two);
        CreateWall(two, three);
        CreateWall(three, four);
        CreateWall(four, one);
    }

    private void CreateWall(Vector2 startCorner, Vector2 endCorner)
    {
        Vector3 start = new Vector3(startCorner.x, 0, startCorner.y);
        GameObject startObj = new GameObject();
        startObj.transform.SetParent(transform);
        Debug.Log(start);
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
        wall.transform.localScale = new Vector3(0.1f, 3f, distance);
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
