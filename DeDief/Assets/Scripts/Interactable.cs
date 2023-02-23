using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    //message displayer when player is looking at an interactable.
    public string promptTitle;
    public string promptDescription;

    //called from player
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    public virtual Interactable GetInteractable() 
    {
        return this;
    }

    protected virtual void Interact()
    {
        //template function to be overridden by subclasses
    }

}
