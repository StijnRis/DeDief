using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskGenerator : MonoBehaviour
{
    public GameObject[] DeskPrefabs;
    
    void Start()
    {
        GameObject Desk = DeskPrefabs[Random.Range(0, DeskPrefabs.Length)];
        GameObject desk = Instantiate(Desk, transform);

        BoxCollider size = desk.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / size.size.x), Mathf.Min(1, maxSize.size.y / size.size.y), Mathf.Min(1, maxSize.size.z / size.size.z));
        desk.transform.localScale = scale;
        size.size = maxSize.size;
    }
}
