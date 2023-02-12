using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairGenerator : MonoBehaviour
{
    // public GameObject[] ChairPrefabs;
    public ItemData[] Chairs;

    void Start()
    {
        // GameObject Chair = ChairPrefabs[Random.Range(0, ChairPrefabs.Length)];
        ItemData chairData = Chairs[Random.Range(0, Chairs.Length)];
        GameObject Chair = chairData.itemPrefab;
        GameObject chair = Instantiate(Chair, transform);

        BoxCollider size = chair.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / size.size.x), Mathf.Min(1, maxSize.size.y / size.size.y), Mathf.Min(1, maxSize.size.z / size.size.z));
        chair.transform.localScale = scale;
        size.size = maxSize.size;
    }
}
