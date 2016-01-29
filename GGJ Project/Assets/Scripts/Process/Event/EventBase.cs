using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    public float existTime;

    WorldBase world;
    Town town;
    GameManager gameMgr;
    TileManager tileMgr;
    Tile currTile;

    public abstract int ID
    {
        get;
    }

    protected int eventIndex; // World's event index
    bool isWaiting = true;
    protected virtual void Awake()
    {
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        tileMgr = GameObject.FindObjectOfType<TileManager>();
    }

    float elapsedTime = 0f;
    protected virtual void Update()
    {
        if (isWaiting)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > existTime)
            {
                DestroyEvent();
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > currTile.clearTime)
            {
                EventCleared();
                elapsedTime = 0f;
            }
        }
    }

    public void SetEvent(int index, WorldBase world, Town town)
    {
        eventIndex = index;
        this.world = world;
        this.town = town;
    }
    
    public void TileAttatched(Tile tile)
    {
        currTile = tile;
        isWaiting = false;
        elapsedTime = 0;
    }

    void EventCleared()
    {
        world.EventDestroyed(eventIndex);
        town.EventCleared();
        gameMgr.EventCleared();
        tileMgr.ResetTile(ID);
        Destroy(this.gameObject);
    }
    
    void DestroyEvent()
    {
        world.EventDestroyed(eventIndex);
        gameMgr.EventDestroyed();
        Destroy(this.gameObject);
    }
}