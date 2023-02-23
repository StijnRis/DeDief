using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingInteractable : Interactable
{
    Interactable parentInteractable;

    public void Set(Interactable parent)
    {
        this.parentInteractable = parent;
    }
    public override Interactable GetInteractable()
    {
        return parentInteractable;
    }
}