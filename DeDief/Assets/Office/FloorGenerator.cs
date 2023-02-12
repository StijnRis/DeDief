using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public Material[] Materials;
    void Start()
    {
        GetComponent<MeshRenderer>().material = Materials[Random.Range(0, Materials.Length)];
    }
}
