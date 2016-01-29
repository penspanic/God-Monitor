using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    public abstract int ID
    {
        get;
    }

    protected virtual void Awake()
    {

    }

    public void SetEvent()
    {
        
    }
}