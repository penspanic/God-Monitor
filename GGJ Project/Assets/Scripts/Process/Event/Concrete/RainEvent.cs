using UnityEngine;
using System.Collections;

public class RainEvent : EventBase
{

    public override int ID
    {
        get
        {
            return 1;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
