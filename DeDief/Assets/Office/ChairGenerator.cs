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

        BoxCollider size = chair.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / size.size.x), Mathf.Min(1, maxSize.size.y / size.size.y), Mathf.Min(1, maxSize.size.z / size.size.z));
        chair.transform.localScale = scale;
        size.size = maxSize.size;
    }
}
