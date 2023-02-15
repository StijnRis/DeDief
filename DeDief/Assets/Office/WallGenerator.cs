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

        PaintingManager paintingManager = GetComponentInParent<PaintingManager>();
        float wallWidth = Box.size.z;
        if (Random.Range(0, 100) < paintingManager.amount && wallWidth > 1.5f)
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
    }

    private void placePainting()
    {
        GameObject painting = Instantiate(Painting, transform);

        painting.transform.localScale = Vector3.one;
        painting.transform.localPosition = Vector3.zero;
        painting.transform.localRotation = Quaternion.identity;

        BoxCollider PaintingSize = painting.GetComponent<BoxCollider>();

        float randomWidth = Random.Range(0.5f, 1.2f);
        Vector3 size = new Vector3(Mathf.Min(0.03f, Box.size.x), Mathf.Min(randomWidth, Box.size.y * 0.7f), Mathf.Min(randomWidth, Box.size.z * 0.7f));
        PaintingSize.size = size;
    }
}
