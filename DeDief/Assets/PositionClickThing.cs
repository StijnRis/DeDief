using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClickThing : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
