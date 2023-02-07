using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeFloorGenerator : MonoBehaviour
{
    public Material[] Materials;
    void Start()
    {
        GetComponent<MeshRenderer>().material = Materials[Random.Range(0, Materials.Length)];
    }
}
