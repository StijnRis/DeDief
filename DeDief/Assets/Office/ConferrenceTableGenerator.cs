using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferrenceTableGenerator : MonoBehaviour
{
    public GameObject[] ConferrenceTablePrefabs;

    void Start()
    {

        GameObject Table = ConferrenceTablePrefabs[Random.Range(0, ConferrenceTablePrefabs.Length)];
        GameObject table = Instantiate(Table, transform);
        BoxCollider box = table.GetComponent<BoxCollider>();
        table.transform.localPosition = new Vector3(0, -box.size.y / 2, 0);

        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(maxSize.size.x / box.size.x, maxSize.size.y / box.size.y,maxSize.size.z / box.size.z);
        table.transform.localScale = scale;
    }
}
