using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 Start;
    public Vector2 End;

    public void setPosition(Vector2 start, Vector2 end)
    {
        float middle = Vector2.Distance(start, end) / 2;
        float size = Mathf.Min(1.0f, Vector2.Distance(start, end) - 0.1f);
        Start = start + (end - start).normalized * (middle - size / 2);
        End = start + (end - start).normalized * (middle + size / 2);
    }
}
