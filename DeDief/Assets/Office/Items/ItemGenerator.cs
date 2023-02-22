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
        SetRotation();
        SetPosition();
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
        if (scaleUp && scaleDown)
        {
            scale = new Vector3(box.size.x / prefabBox.size.x, box.size.y / prefabBox.size.y, box.size.z / prefabBox.size.z);
        }
        else if (scaleDown)
        {
            scale = new Vector3(Mathf.Min(1, box.size.x / prefabBox.size.x), Mathf.Min(1, box.size.y / prefabBox.size.y), Mathf.Min(1, box.size.z / prefabBox.size.z));
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
        Vector3 position = new Vector3(0, -prefabBox.center.y * prefab.transform.lossyScale.y, 0);
        Vector3 size = transform.rotation * Vector3.Scale(box.size, transform.lossyScale);
        Vector3 prefabSize = prefab.transform.rotation * Vector3.Scale(prefabBox.size, prefab.transform.lossyScale);
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
}
