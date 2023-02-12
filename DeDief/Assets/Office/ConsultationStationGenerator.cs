using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultationStationGenerator : MonoBehaviour
{
    public GameObject Table;
    public GameObject Chair;

    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        GameObject table = Instantiate(Table, transform);
        BoxCollider DeskSize = table.GetComponent<BoxCollider>();
        table.transform.localPosition = new Vector3(0, -box.size.y / 2 + DeskSize.size.y / 2, 0);

        DeskSize.size = new Vector3(box.size.x - 1f, 1, box.size.z);
    }
}
