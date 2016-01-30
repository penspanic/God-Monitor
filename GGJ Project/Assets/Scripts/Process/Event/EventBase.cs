using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    public float existTime;

    World world;
    public Town town
    {
        get;
        private set;
    }
    GameManager gameMgr;
    TileManager tileMgr;
    public Tile currTile;

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

    public void SetEvent(int index, World world, Town town)
    {
        eventIndex = index;
        this.world = world;
        this.town = town;
        town.ShowMessage(this);
    }
    
    public void TileAttatched(Tile tile)
    {
        tileMgr.ShowCoolTime(tile);
        currTile = tile;
        currTile.UseTile(this);
        currTile.GetComponent<BoxCollider2D>().enabled = false;
        currTile.isAttatched = true;
        isWaiting = false;
        elapsedTime = 0;
    }

    public void GoatUse()
    {
        world.EventDestroyed(eventIndex);
        town.EventCleared();
        gameMgr.EventCleared();
        Destroy(this.gameObject);
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
        town.EventDestroyed();
        gameMgr.EventDestroyed();
        Destroy(this.gameObject);
    }
}