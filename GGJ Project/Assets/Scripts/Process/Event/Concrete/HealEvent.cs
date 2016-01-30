using UnityEngine;
using System.Collections;

public class HealEvent : EventBase
{

    public override int ID
    {
        get
        {
            return 5;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
