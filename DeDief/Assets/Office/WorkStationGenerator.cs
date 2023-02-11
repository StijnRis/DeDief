using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationGenerator : MonoBehaviour
{
    public GameObject Desk;
    public GameObject Chair;

    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        GameObject desk = Instantiate(Desk, transform);
        desk.transform.localPosition = new Vector3(0, -box.size.y / 2, box.size.z / 4);
        desk.transform.localRotation = Quaternion.Euler(0, 180, 0);
        BoxCollider deskBox = desk.GetComponent<BoxCollider>();
        deskBox.size = new Vector3(box.size.x, box.size.y, box.size.z / 2);
        deskBox.center = new Vector3(0, box.size.y / 2, 0);

        GameObject chair = Instantiate(Chair, transform);
        chair.transform.localPosition = new Vector3(0, -box.size.y / 2, -box.size.z / 4);
        BoxCollider chairBox = chair.GetComponent<BoxCollider>();
        chairBox.size = new Vector3(box.size.x, box.size.y, box.size.z / 2);
        chairBox.center = new Vector3(0, box.size.y / 2, 0);
    }
}
