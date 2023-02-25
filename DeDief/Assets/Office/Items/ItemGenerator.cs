using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public bool scaleDown = true;
    public bool scaleUp = false;
    public bool keepAspectRatio = false;
    [Range(0.0f, 180f)]
    public float maxOrientationOffset = 0;
    public bool randomPosition = true;
    public GameObject[] Prefabs;

    protected BoxCollider box;
    protected GameObject prefab;
    protected BoxCollider prefabBox;

    // for easier screenshotting
    public bool forceSpecificItem = false;
    public int itemToForce = 0;

    void Start()
    {
        box = GetComponent<BoxCollider>();
        placeRandom();

        SetScale();
        SetPosition();
        SetRotation();
    }

    protected void placeRandom()
    {
        GameObject StorageBox;
        if (forceSpecificItem)
        {
            StorageBox = Prefabs[itemToForce];
        }
        else
        {
            StorageBox = Prefabs[Random.Range(0, Prefabs.Length)];
        }
        prefab = Instantiate(StorageBox, transform);
        prefabBox = prefab.GetComponent<BoxCollider>();
        if (prefabBox == null)
        {
            prefabBox = prefab.AddComponent<BoxCollider>();
        }
    }

    protected void SetScale()
    {
        Vector3 scale = Vector3.one;
        Vector3 size = prefab.transform.localRotation * box.size;
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
        Vector3 prefabSize = prefabBox.size;
        if (scaleUp && scaleDown)
        {
            scale = new Vector3(size.x / prefabSize.x, size.y / prefabSize.y, size.z / prefabSize.z);
        }
        else if (scaleDown)
        {
            scale = new Vector3(Mathf.Min(1, size.x / prefabSize.x), Mathf.Min(1, size.y / prefabSize.y), Mathf.Min(1, size.z / prefabSize.z));
        }
        if (keepAspectRatio)
        {
            float minScale = Mathf.Min(scale.x, scale.y, scale.z);
            scale.x = minScale;
            scale.y = minScale;
            scale.z = minScale;
        }
        prefab.transform.localScale = scale;
    }

    protected void SetRotation()
    {
        Quaternion rotation = Quaternion.Euler(prefab.transform.localRotation * new Vector3(0, Random.Range(0, maxOrientationOffset) - Random.Range(0, maxOrientationOffset), 0));
        prefab.transform.localRotation *= rotation;
    }

    protected void SetPosition()
    {
        Vector3 position = new Vector3(0, -(prefab.transform.localRotation * prefabBox.center).y * (prefab.transform.localRotation * prefab.transform.lossyScale).y, 0);
        Vector3 size = Vector3.Scale(box.size, transform.lossyScale);
        Vector3 prefabSize = prefab.transform.localRotation * Vector3.Scale(prefabBox.size, prefab.transform.lossyScale);
        prefabSize = new Vector3(Mathf.Abs(prefabSize.x), Mathf.Abs(prefabSize.y), Mathf.Abs(prefabSize.z));
        if (randomPosition)
        {
            Vector3 extraRoom = size - prefabSize;
            position.x += Random.Range(-extraRoom.x / 2, extraRoom.x / 2);
            position.z += Random.Range(-extraRoom.z / 2, extraRoom.z / 2);
        }
              
        if (!scaleUp)
        {
            position += new Vector3(0, (prefabSize.y - size.y) / 2, 0);
        }
        prefab.transform.localPosition = position;
    }

    public Interactable GetInteractable()
    {
        Interactable interactable = prefab.GetComponent<Item>();
        return interactable;
    }
}
