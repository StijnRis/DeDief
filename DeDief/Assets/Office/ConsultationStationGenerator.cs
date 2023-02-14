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

        Vector3 chairSize = new Vector3(1, 2, 1);
        Vector3 deskSize = new Vector3(box.size.x - chairSize.x * 2, 0.7f, box.size.z);

        GameObject table = Instantiate(Table, transform);
        BoxCollider DeskSize = table.GetComponent<BoxCollider>();
        DeskSize.size = deskSize;

        table.transform.localPosition = new Vector3(0, -box.size.y / 2 + DeskSize.size.y / 2, 0);

        int amount_of_chairs = Mathf.FloorToInt(box.size.z / 1);
        float x1 = DeskSize.size.x / 2 + chairSize.x / 2;
        float x2 = -x1;
        for (int i = 0; i < amount_of_chairs; i++)
        {
            float z = -0.5f * (amount_of_chairs - 1) + chairSize.z * i;

            GameObject chair1 = Instantiate(Chair, transform);
            chair1.transform.localPosition = new Vector3(x1, -box.size.y / 2 + chairSize.y / 2, z);
            chair1.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            BoxCollider chair1Box = chair1.GetComponent<BoxCollider>();
            chair1Box.size = chairSize;

            GameObject chair2 = Instantiate(Chair, transform);
            chair2.transform.localPosition = new Vector3(x2, -box.size.y / 2 + chairSize.y / 2, z);
            chair2.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            BoxCollider chair2Box = chair2.GetComponent<BoxCollider>();
            chair2Box.size = chairSize;
        }
    }
}
