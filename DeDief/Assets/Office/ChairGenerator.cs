using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairGenerator : MonoBehaviour
{
    public GameObject[] Chairs;

    void Start()
    {
        GameObject chair = Chairs[Random.Range(0, Chairs.Length)];
        Instantiate(chair, transform);
    }
}
