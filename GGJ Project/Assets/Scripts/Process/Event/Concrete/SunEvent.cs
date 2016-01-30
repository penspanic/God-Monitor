using UnityEngine;
using System.Collections;

public class SunEvent : EventBase
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
