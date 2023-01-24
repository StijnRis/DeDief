using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    static CorridorTypes getRandomType()
    {
        int type = Random.Range(1, 100);
        if (type <= 50)
        {
            return CorridorTypes.Straight;
        } else if (type <= 55)
        {
            return CorridorTypes.TJunction;
        } else if (type <= 60)
        {
            return CorridorTypes.Junction;
        } else {
            return CorridorTypes.OfficeDoor;
        }
    }
}

enum CorridorTypes
{
    Straight,
    TJunction,
    Junction,
    OfficeDoor
}