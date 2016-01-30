using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    public int ID;
    public float clearTime;
    public bool isAttatched = false;
    void Start()
    {

    }

    void Update()
    {

    }

    public virtual void UseTile(EventBase targetEvent)
    {

    }

    public static bool CompareID(Tile tile, EventBase targetEvent)
    {
        return tile.ID == targetEvent.ID;
    }
}