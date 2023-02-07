using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairGenerator : MonoBehaviour
{
    public GameObject Chair;

    void Start()
    {
        Instantiate(Chair, transform);
    }
}
