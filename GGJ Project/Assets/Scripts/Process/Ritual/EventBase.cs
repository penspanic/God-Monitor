using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    EventManager eventMgr;
    public abstract int ID
    {
        get;
    }

    protected int eventIndex; // EventManager's event index
    protected virtual void Awake()
    {
        eventMgr = GameObject.FindObjectOfType<EventManager>();
    }

    public void SetEvent(int index)
    {
        eventIndex = index;
    }

    void DestroyEvent()
    {
        eventMgr.isEventCreated[eventIndex] = false;
    }
}