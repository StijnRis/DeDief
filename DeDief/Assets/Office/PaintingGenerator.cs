using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaintingGenerator : MonoBehaviour
{
    private GameObject paintingFrame;
    private string paintingPath = "/Office/Decoration/paintings/";

    private Size size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<Size>();
        transform.localPosition = Vector3.zero + new Vector3(-size.size.x * 2, 0, 0);
        transform.localRotation = Quaternion.identity;

        Generate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Generate()
    {
        GameObject paintingFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paintingFrame.transform.SetParent(transform);
        paintingFrame.transform.localPosition = Vector3.zero;
        paintingFrame.transform.localScale = size.size;
        paintingFrame.transform.localRotation = Quaternion.identity;

        GameObject paintingCanvas = GameObject.CreatePrimitive(PrimitiveType.Plane);
        paintingCanvas.transform.SetParent(transform);
        paintingCanvas.transform.localPosition = Vector3.forward;
        paintingCanvas.transform.localScale = size.size;
        paintingCanvas.transform.localRotation = Quaternion.identity;

        Vector3 scaled_size = new Vector3(size.size.x, size.size.y, size.size.z);
        paintingCanvas.transform.localScale = Vector3.Scale(scaled_size, new Vector3(0.9f, 0.9f, 0.9f));

        var texture = getRandomPaintingTexture();
        paintingCanvas.GetComponent<Renderer>().material.mainTexture = texture;
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
