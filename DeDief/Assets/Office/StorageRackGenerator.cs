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

        rack.transform.localPosition = new Vector3(0, -1, 0);
        rack.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));

        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.z / box.size.x, maxSize.size.y / box.size.y, maxSize.size.x / box.size.z);
        /*scale = new Vector3(1, 1, 1);*/
        rack.transform.localScale = scale;

        
    }
}
