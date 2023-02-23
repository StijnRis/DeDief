using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaintingGenerator : MonoBehaviour
{
    private GameObject paintingFrame;
    private float paintingDepth = 0.1f;

    // Item attributes
    [Header("Item settings")]
    public new string name = "Painting";
    public int moneyValue;
    
    [Header("In grid settings")]
    [SerializeField] protected GameObject itemPanel;
    private int width = 0;
	private int height = 0;
	private Sprite itemIcon;
	private bool canBeRotated = true;

    private Vector3 size;

    private System.Random random = new System.Random();

    void Start()
    {
        size = GetComponent<BoxCollider>().size;
        transform.localPosition = Vector3.zero + new Vector3(-size.x * 2, 0, 0);
        transform.localRotation = Quaternion.identity;

        Generate();
    }

    void Generate()
    {
        GameObject paintingFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paintingFrame.layer = LayerMask.NameToLayer("Interactables");
        paintingFrame.transform.SetParent(transform);
        paintingFrame.transform.localPosition = Vector3.zero;
        paintingFrame.transform.localScale = size;
        paintingFrame.transform.localRotation = Quaternion.identity;

        GameObject paintingCanvas = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paintingCanvas.layer = LayerMask.NameToLayer("Interactables");
        paintingCanvas.transform.SetParent(paintingFrame.transform);
        paintingCanvas.transform.localPosition = Vector3.zero + new Vector3(-0.5f - paintingDepth, 0, 0);
        paintingCanvas.transform.localScale = new Vector3(paintingDepth, 0.9f, 0.9f);
        paintingCanvas.transform.localRotation = Quaternion.identity;

        PaintingManager paintingManager = GetComponentInParent<PaintingManager>();
        Texture2D texture = paintingManager.getRandomPaintingTexture();
        paintingCanvas.GetComponent<Renderer>().material.mainTexture = texture;

        Item paintingItem = paintingFrame.AddComponent<Item>();
        itemIcon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
        width = texture.width / 256;
        height = texture.height / 256;
        moneyValue = paintingItem.RandomizeMoneyValue(moneyValue);
        paintingItem.Set(name, width, height, itemIcon, canBeRotated, moneyValue, itemPanel);
        PaintingInteractable paintingInteractable = paintingCanvas.AddComponent<PaintingInteractable>();
        paintingInteractable.Set(paintingItem);
    }

}
