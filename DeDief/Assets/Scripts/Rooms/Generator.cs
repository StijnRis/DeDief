using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Generator : MonoBehaviour
{
    protected Size Size;

    public void Start()
    {
        Size = GetComponent<Size>();
        Generate();
    }

    public abstract void Generate();

    public void OnDestroy()
    {
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
    }
}
