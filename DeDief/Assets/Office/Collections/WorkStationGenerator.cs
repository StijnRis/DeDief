using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationGenerator : MonoBehaviour
{
    public GameObject Desk;
    public GameObject Chair;
    public GameObject Laptop;
    public GameObject Pen;

    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        GameObject desk = Instantiate(Desk, transform);
        desk.transform.localPosition = new Vector3(0, 0, box.size.z / 4);
        desk.transform.localRotation = Quaternion.Euler(0, 0, 0);
        BoxCollider deskBox = desk.GetComponent<BoxCollider>();
        deskBox.size = new Vector3(box.size.x, 0.6f * box.size.y, box.size.z / 2);

        float y = desk.transform.localPosition.y + deskBox.size.y / 2;

        GameObject laptop = Instantiate(Laptop, transform);
        BoxCollider laptopBox = laptop.GetComponent<BoxCollider>();
        laptopBox.size = new Vector3(0.5f, 0.3f, 0.3f);
        laptop.transform.localPosition = new Vector3(0, y + laptopBox.size.y / 2, box.size.z / 4);
        laptop.transform.localRotation = Quaternion.Euler(0, 180, 0);

        GameObject pen = Instantiate(Pen, transform);
        BoxCollider penBox = pen.GetComponent<BoxCollider>();
        penBox.size = new Vector3(0.1f, 0.01f, 0.01f);
        pen.transform.localPosition = new Vector3(laptopBox.size.x / 2 + 0.1f, y + penBox.size.y / 2, box.size.z / 4);
        pen.transform.localRotation = Quaternion.Euler(0, 180, 0);

        GameObject chair = Instantiate(Chair, transform);
        chair.transform.localPosition = new Vector3(0, 0, -box.size.z / 4);
        BoxCollider chairBox = chair.GetComponent<BoxCollider>();
        chairBox.size = new Vector3(box.size.x, box.size.y, box.size.z / 2);
    }
}
