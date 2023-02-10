using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject Painting;
    public Material[] Materials;
    protected BoxCollider Box;

    void Start()
    {
        Box = GetComponent<BoxCollider>();
        Generate();
    }

    public void Generate()
    {
        placeWall();

        if (Random.Range(0,5) == 1 )
        {
            placePainting();
        }
    }

    private void placeWall()
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.GetComponent<MeshRenderer>().material = Materials[Random.Range(0, Materials.Length)];
        wall.transform.SetParent(transform);

        wall.transform.localScale = Box.size;
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

        BoxCollider PaintingSize = painting.GetComponent<BoxCollider>();
        Vector3 size = new Vector3(Mathf.Min(0.03f, Box.size.x), Mathf.Min(0.5f, Box.size.y), Mathf.Min(0.5f, Box.size.z));
        PaintingSize.size = size;
    }
}
