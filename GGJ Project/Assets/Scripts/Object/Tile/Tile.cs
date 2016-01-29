using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    public int ID;
    public float clearTime;

    void Start()
    {

    }

    void Update()
    {

    }

    public static bool CompareID(Tile tile, EventBase targetEvent)
    {
        return tile.ID == targetEvent.ID;
    }
}