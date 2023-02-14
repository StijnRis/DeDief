using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 Start;
    public Vector3 End;

    public void setPosition(Vector3 start, Vector3 end)
    {
        float middle = Vector3.Distance(start, end) / 2;
        float size = Mathf.Min(1.0f, Vector3.Distance(start, end) - 0.1f);
        Start = start + (end - start).normalized * (middle - size / 2);
        End = start + (end - start).normalized * (middle + size / 2);
    }
}
