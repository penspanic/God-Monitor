using UnityEngine;
using System.Collections;

public class ThunderEvent : EventBase
{

    public override int ID
    {
        get
        {
            return 3;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
