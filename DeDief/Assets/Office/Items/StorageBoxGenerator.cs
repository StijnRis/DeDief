using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBoxGenerator : ItemGenerator
{
    /*public GameObject[] boxPrefabs;*/

    void Start()
    {
        placeRandom();
        Scale();
        RandomRotation();
    }
}
