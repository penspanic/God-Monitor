using UnityEngine;
using System.Collections;

public class HarvestEvent : EventBase
{

    public override int ID
    {
        get
        {
            return 4;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
