using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class EventA : EventBase
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