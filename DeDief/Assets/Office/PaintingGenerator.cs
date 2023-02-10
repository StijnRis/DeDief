using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaintingGenerator : MonoBehaviour
{
    private GameObject painting;
    private string paintingPath = "/Office/Decoration/paintings/";

    protected Size size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<Size>();

        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        painting = GameObject.CreatePrimitive(PrimitiveType.Cube);
        painting.transform.SetParent(transform);

        painting.transform.localScale = size.size;

        float size_x = size.size.x;
        painting.transform.localPosition = Vector3.zero + new Vector3(-size_x * 2, 0, 0);
        painting.transform.localRotation = Quaternion.identity;

        var texture = getRandomPaintingTexture();
        painting.GetComponent<Renderer>().material.mainTexture = texture;
    }

    Texture2D getRandomPaintingTexture()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + paintingPath);
        var files = dir.GetFiles("painting_*.jpg");
        int randomIndex = Random.Range(0, files.Length);
        var randomFile = files[randomIndex];
        string path = randomFile.FullName;

        var bytes = System.IO.File.ReadAllBytes(path);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(bytes);

        return tex;
    }
}
