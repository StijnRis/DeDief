using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskGenerator : MonoBehaviour
{
    public GameObject Desk;
    void Start()
    {
        GameObject desk = Instantiate(Desk, transform);
    }
}
