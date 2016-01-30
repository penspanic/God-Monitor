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

    static GameObject messagePrefab;
  
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

        if (messagePrefab == null)
            messagePrefab = Resources.Load<GameObject>("Prefabs/Message");

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

        //GameObject messageObj = Instantiate<GameObject>(messagePrefab);
        //messageObj.transform.SetParent(this.transform);
        //messageObj.transform.localPosition = new Vector2(0, -9.8f);
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