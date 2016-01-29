using UnityEngine;
using System.Collections;

public class EventB : EventBase
{

    public override int ID
    {
        get
        {
            return 2;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
