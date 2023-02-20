using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public bool scaleDown = true;
    public bool scaleUp = false;
    [Range(0.0f, 180f)]
    public float randomOrientationOffset = 0;
    public GameObject[] Prefabs;

    protected GameObject prefab;
    protected BoxCollider prefabBox;
    protected BoxCollider box;

    void Start()
    {
        box = GetComponent<BoxCollider>();
        placeRandom();
        if (scaleUp && scaleDown)
        {
            Scale();
        } else if (scaleDown)
        {
            ScaleDown();
        } else if (scaleUp)
        {
            throw new System.Exception("NOT implemented");
        }
        
        RandomRotation(randomOrientationOffset);
        setPosition();
    }

    protected void placeRandom()
    {
        GameObject StorageBox = Prefabs[Random.Range(0, Prefabs.Length)];
        prefab = Instantiate(StorageBox, transform);
        prefabBox = prefab.GetComponent<BoxCollider>();
        if (prefabBox == null)
        {
            prefabBox = prefab.AddComponent<BoxCollider>();
        }
    }

    protected void ScaleDown()
    {
        Vector3 scale = new Vector3(Mathf.Min(1, box.size.x / prefabBox.size.x), Mathf.Min(1, box.size.y / prefabBox.size.y), Mathf.Min(1, box.size.z / prefabBox.size.z));
        prefab.transform.localScale = scale;
    }

    protected void Scale()
    {
        Vector3 scale = new Vector3(box.size.x / prefabBox.size.x, box.size.y / prefabBox.size.y, box.size.z / prefabBox.size.z);
        prefab.transform.localScale = scale;
    }

    protected void RandomRotation(float maxOffset)
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(-maxOffset, maxOffset), 0);
        prefab.transform.localRotation = rotation;
    }

    protected void setPosition()
    {
        Vector3 position = new Vector3(0, -prefabBox.center.y * prefab.transform.lossyScale.y, 0);
              
        if (!scaleUp)
        {
            float size = prefab.GetComponent<BoxCollider>().size.y * prefab.transform.lossyScale.y;
            float maxSize = GetComponent<BoxCollider>().size.y * transform.lossyScale.y;
            position += new Vector3(0, (size - maxSize) / 2, 0);
        }
        prefab.transform.localPosition = position;
    }
}
