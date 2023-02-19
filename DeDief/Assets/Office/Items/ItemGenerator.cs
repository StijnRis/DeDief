using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public bool scaleUp;
    [Range(0.0f, 180f)]
    public float randomOrientationOffset = 0;
    public GameObject[] Prefabs;

    protected GameObject prefab;

    void Start()
    {
        placeRandom();
        if (scaleUp)
        {
            Scale();
        } else
        {
            ScaleDown();
        }
        
        RandomRotation(randomOrientationOffset);
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

    protected void RandomRotation()
    {
        RandomRotation(180);
    }

    protected void RandomRotation(float maxOffset)
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(-maxOffset, maxOffset), 0);
        prefab.transform.localRotation = rotation;
    }
}
