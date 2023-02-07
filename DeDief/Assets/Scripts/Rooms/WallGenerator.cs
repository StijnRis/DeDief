using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject Painting;
    protected Size Size;
    void Start()
    {
        Size = GetComponent<Size>();
        Generate();
    }

    public void Generate()
    {
        placeWall();
        placePainting();
    }

    private void placeWall()
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(transform);

        wall.transform.localScale = Size.size;
        wall.transform.localPosition = Vector3.zero;
        wall.transform.localRotation = Quaternion.identity;

        wall.layer = LayerMask.NameToLayer("Walls");
        wall.tag = "Wall";
    }

    private void placePainting()
    {
        GameObject painting = Instantiate(Painting, transform);

        painting.transform.localScale = Vector3.one;
        painting.transform.localPosition = Vector3.zero;
        painting.transform.localRotation = Quaternion.identity;

        Size PaintingSize = painting.GetComponent<Size>();
        Vector3 size = new Vector3(Mathf.Min(0.5f, Size.size.x), Mathf.Min(0.5f, Size.size.y), Mathf.Min(0.5f, Size.size.z));
        PaintingSize.size = size;
    }
}
