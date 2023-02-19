using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaintingGenerator : MonoBehaviour
{
    private GameObject paintingFrame;
    private float paintingDepth = 0.1f;

    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<BoxCollider>().size;
        transform.localPosition = Vector3.zero + new Vector3(-size.x * 2, 0, 0);
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
        paintingFrame.transform.localScale = size;
        paintingFrame.transform.localRotation = Quaternion.identity;

        GameObject paintingCanvas = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paintingCanvas.transform.SetParent(paintingFrame.transform);
        paintingCanvas.transform.localPosition = Vector3.zero + new Vector3(-0.5f - paintingDepth, 0, 0);
        paintingCanvas.transform.localScale = new Vector3(paintingDepth, 0.9f, 0.9f);
        paintingCanvas.transform.localRotation = Quaternion.identity;

        PaintingManager paintingManager = GetComponentInParent<PaintingManager>();
        Texture2D texture = paintingManager.getRandomPaintingTexture();
        paintingCanvas.GetComponent<Renderer>().material.mainTexture = texture;
    }

}
