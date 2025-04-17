using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    public string PMessage;

    public void BaseInteract()
    {
        InteraWithObject();
    }

    protected virtual void InteraWithObject()
    {

    }
}
