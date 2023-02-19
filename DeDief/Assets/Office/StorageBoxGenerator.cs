using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBoxGenerator : MonoBehaviour
{
    public GameObject[] boxPrefabs;

    void Start()
    {
        GameObject StorageBox = boxPrefabs[Random.Range(0, boxPrefabs.Length)];
        GameObject storageBox = Instantiate(StorageBox, transform);
        
        BoxCollider box = storageBox.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.x / box.size.x, maxSize.size.y / box.size.y, maxSize.size.z / box.size.z);
        storageBox.transform.localScale = scale;

        storageBox.transform.localPosition = new Vector3(0, -box.center.y * scale.y, 0);

        Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        storageBox.transform.localRotation = rotation;
    }
}
