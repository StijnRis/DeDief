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

    void Start()
    {
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
    }

    protected void ScaleDown()
    {
        BoxCollider box = prefab.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / box.size.x), Mathf.Min(1, maxSize.size.y / box.size.y), Mathf.Min(1, maxSize.size.z / box.size.z));
        prefab.transform.localScale = scale;
    }

    protected void Scale()
    {
        BoxCollider box = prefab.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.x / box.size.x, maxSize.size.y / box.size.y, maxSize.size.z / box.size.z);
        prefab.transform.localScale = scale;
    }

    protected void RandomRotation(float maxOffset)
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(-maxOffset, maxOffset), 0);
        prefab.transform.localRotation = rotation;
    }

    protected void setPosition()
    {
        BoxCollider prefabBox = prefab.GetComponent<BoxCollider>();
        Vector3 position = new Vector3(0, -prefabBox.center.y * prefab.transform.lossyScale.y, 0);
              
        if (!scaleUp)
        {
            float size = prefab.GetComponent<BoxCollider>().size.y;
            float maxSize = GetComponent<BoxCollider>().size.y;
            position += new Vector3(0, (size - maxSize) / 2, 0);
        }
        prefab.transform.localPosition = position;
    }
}
