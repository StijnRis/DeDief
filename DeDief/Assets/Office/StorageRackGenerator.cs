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

        BoxCollider roomSize = GetComponent<BoxCollider>();
        rack.transform.localPosition = new Vector3(0, -1, 0);
    }
}
