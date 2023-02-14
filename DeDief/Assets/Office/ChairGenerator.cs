using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairGenerator : MonoBehaviour
{
    public ItemData[] Chairs;

    void Start()
    {
        ItemData chairData = Chairs[Random.Range(0, Chairs.Length)];
        GameObject Chair = chairData.itemPrefab;
        GameObject chair = Instantiate(Chair, transform);

        BoxCollider box = chair.GetComponent<BoxCollider>();
        BoxCollider maxSize = GetComponent<BoxCollider>();
        Vector3 scale = new Vector3(Mathf.Min(1, maxSize.size.x / box.size.x), Mathf.Min(1, maxSize.size.y / box.size.y), Mathf.Min(1, maxSize.size.z / box.size.z));
        chair.transform.localScale = scale;
        chair.transform.localPosition = new Vector3(0, -GetComponent<BoxCollider>().size.y / 2, 0);
    }
}
