using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBoxGenerator : MonoBehaviour
{
    public GameObject[] boxPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject StorageBox = boxPrefabs[Random.Range(0, boxPrefabs.Length)];
        GameObject storageBox = Instantiate(StorageBox, transform);
        BoxCollider box = storageBox.AddComponent<BoxCollider>();

        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.x / box.size.x, maxSize.size.y / box.size.y, maxSize.size.z / box.size.z);
        storageBox.transform.localScale = scale;

        storageBox.transform.localPosition = Vector3.zero;
        storageBox.transform.localPosition = new Vector3(0, -box.center.y * scale.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
