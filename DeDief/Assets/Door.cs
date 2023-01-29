using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 Start;
    public Vector2 End;

    public void placeBetween(Vector2 start, Vector2 end)
    {
        float middle = Vector2.Distance(start, end) / 2;
        Start = start + (end - start).normalized * (middle - 0.5f);
        End = start + (end - start).normalized * (middle + 0.5f);
    }
}
