using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;

    void Start()
    {
        Vector3 size = GetComponentInParent<BoxCollider>().size;
        
        if (size.z > size.x)
        {
            GameObject door = Instantiate(Door, transform);
            door.transform.localRotation = Quaternion.Euler(0, -90, 0);
            door.transform.localPosition = new Vector3(0, 0, size.z / 2);
            Player.transform.position = transform.position + transform.rotation * new Vector3(0, 0, size.z / 2 - 2f);
        }
        else
        {
            GameObject door = Instantiate(Door, transform);
            door.transform.localRotation = Quaternion.Euler(0, 0, 0);
            door.transform.localPosition = new Vector3(size.x / 2, 0, 0);
            Player.transform.position = transform.position + transform.rotation * new Vector3(size.x / 2 - 2f, 0, 0);
        }
            Debug.Log(Player.transform.position);
    }
}
