using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.SetAsFirstSibling();
    }
}
