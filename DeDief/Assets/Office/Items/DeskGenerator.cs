using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskGenerator : ItemGenerator
{
    
    void Start()
    {
        placeRandom();
        Scale();
        RandomRotation(2);
    }
}
