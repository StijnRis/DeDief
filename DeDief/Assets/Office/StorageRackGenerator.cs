using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRackGenerator : MonoBehaviour
{
    public GameObject[] rackPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Rack = rackPrefabs[Random.Range(0, rackPrefabs.Length)];
        GameObject rack = Instantiate(Rack, transform);
        BoxCollider box = rack.GetComponent<BoxCollider>();

        rack.transform.localPosition = new Vector3(0, -box.size.y / 2, 0);
        
        Vector3 rackRotation = new Vector3(0, -90, 0);
        rack.transform.Rotate(rackRotation);

        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.x / box.size.x, maxSize.size.y / box.size.y, maxSize.size.z / box.size.z);
        rack.transform.localScale = scale;

        
    }
}
