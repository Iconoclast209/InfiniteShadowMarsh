using System;
using UnityEngine;

/// <summary>Base class for all pick-ups.</summary>
public abstract class PickUp : ScriptableObject
{
    /// <summary>Abstract method.  Meant to be over-ridden by derived classes to create unique pick-up functionality.</summary>
    public abstract void Action();

    /// <summary>Virtual method.  Meant to be over-ridden by derived classes to give pick-ups unique "set-up" functionality.<para>Virtual to prevent need to over-ride in derived class unless wanted.</para></summary>
    public virtual void SetUp()
    {
        return;
    }
}
