using UnityEngine;
using System.Collections;

public class World1 : WorldBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override EventBase GetNewEvent()
    {
        return Instantiate<GameObject>(eventPrefabs[0]).GetComponent<EventBase>();
    }

    public override void WorldActivate()
    {
        base.WorldActivate();
    }

    public override void WorldInactivate()
    {
        base.WorldInactivate();
    }
}
