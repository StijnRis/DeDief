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
        float value = GetComponent<Value>().value;

        BoxCollider box = GetComponent<BoxCollider>();

        GameObject desk = Instantiate(Desk, transform);
        desk.AddComponent<Value>().value = 0.1f * value;
        BoxCollider deskBox = desk.GetComponent<BoxCollider>();
        deskBox.size = new Vector3(box.size.x, 0.6f * box.size.y, box.size.z / 2);
        desk.transform.localPosition = new Vector3(0, -box.size.y / 2 + deskBox.size.y / 2, box.size.z / 4);
        desk.transform.localRotation = Quaternion.Euler(0, 0, 0);

        float y = desk.transform.localPosition.y + deskBox.size.y / 2;

        if (value > 50)
        {
            GameObject laptop = Instantiate(Laptop, transform);
            laptop.AddComponent<Value>().value = 0.5f * value;
            BoxCollider laptopBox = laptop.GetComponent<BoxCollider>();
            laptopBox.size = new Vector3(0.5f, 0.3f, 0.3f);
            laptop.transform.localPosition = new Vector3(0, y + laptopBox.size.y / 2, box.size.z / 4);
            laptop.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        GameObject pen = Instantiate(Pen, transform);
        pen.AddComponent<Value>().value = 0.2f * value;
        BoxCollider penBox = pen.GetComponent<BoxCollider>();
        penBox.size = new Vector3(0.1f, 0.01f, 0.01f);
        pen.transform.localPosition = new Vector3(0.35f, y + penBox.size.y / 2, box.size.z / 4);
        pen.transform.localRotation = Quaternion.Euler(0, 180, 0);

        GameObject chair = Instantiate(Chair, transform);
        chair.AddComponent<Value>().value = 0.4f * value;
        BoxCollider chairBox = chair.GetComponent<BoxCollider>();
        chairBox.size = new Vector3(box.size.x, box.size.y, box.size.z / 2);
        chair.transform.localPosition = new Vector3(0, 0, -box.size.z / 4);
    }
}
