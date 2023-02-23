using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject Door;

    void Start()
    {
        
        Vector3 size = transform.parent.localRotation * -GetComponentInParent<BoxCollider>().size;
        GameObject door = Instantiate(Door, transform);
        door.transform.localRotation = Quaternion.Euler(0, 90, 0);
        door.transform.localPosition = new Vector3(0, 0, size.z / 2);
        /*if (size.x > size.z)
        {
            GameObject door = Instantiate(Door, transform);
            door.transform.rotation = Quaternion.Euler(0, 90, 0);
            door.transform.position = new Vector3(size.x / 2, 0, 0);
        } else
        {
            GameObject door = Instantiate(Door, transform);
            door.transform.rotation = Quaternion.Euler(0, 0, 0);
            door.transform.position = new Vector3(0, 0, size.z / 2);
        }*/
    }
}
