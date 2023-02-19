using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairGenerator : MonoBehaviour
{
    public GameObject[] ChairPrefabs;

    void Start()
    {
        GameObject Chair = ChairPrefabs[Random.Range(0, ChairPrefabs.Length)];
        GameObject chair = Instantiate(Chair, transform);

        BoxCollider box = chair.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / box.size.x), Mathf.Min(1, maxSize.size.y / box.size.y), Mathf.Min(1, maxSize.size.z / box.size.z));
        chair.transform.localScale = scale;

        float xOffset = (Random.Range(0, 2) * 2 - 1) * Random.Range(0, (maxSize.size.x - box.size.x) / 2);
        float zOffset = (Random.Range(0, 2) * 2 - 1) * Random.Range(0, (maxSize.size.z - box.size.z) / 2);
        chair.transform.localPosition = new Vector3(xOffset, -GetComponent<BoxCollider>().size.y / 2, zOffset);

        Quaternion rotation = Quaternion.Euler(0, Random.Range(-10f, 10f), 0);
        chair.transform.localRotation = rotation;
    }
}