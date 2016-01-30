using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class WorldBase : MonoBehaviour
{

    protected static GameObject[] eventPrefabs;
    Town[] towns;
    bool[] isEventCreated;

    protected virtual void Awake()
    {
        List<Town> townList = new List<Town>();
        for (int i = 1; i < 4; i++)
            townList.Add(transform.FindChild(i.ToString()).GetComponent<Town>());
        towns = townList.ToArray();
        isEventCreated = new bool[towns.Length];

        if (eventPrefabs == null)
            eventPrefabs = Resources.LoadAll<GameObject>("Prefabs/Events");
    }

    protected virtual void Update()
    {

    }

    public void CreateEvent()
    {
        int createIndex = GetCreateEventIdnex();

        EventBase newEvent = GetNewEvent();
        newEvent.transform.SetParent(towns[createIndex].transform);
        newEvent.transform.localPosition = Vector2.zero;
        isEventCreated[createIndex] = true;

        newEvent.SetEvent(createIndex, this, towns[createIndex]);
    }

    public int GetCreateEventIdnex()
    {
        int createIndex = 0;
        while (true)
        {
            createIndex = Random.Range(0, towns.Length);
            if (isEventCreated[createIndex])
                continue;
            else
                break;

        }
        return createIndex;
    }

    public bool IsEmptyPlaceExist()
    {
        bool isEmptyPlaceExist = false;
        for (int i = 0; i < isEventCreated.Length; i++)
        {
            if (!isEventCreated[i])
                isEmptyPlaceExist = true;
        }
        return isEmptyPlaceExist;
    }

    EventBase GetNewEvent()
    {
        return Instantiate<GameObject>(eventPrefabs[Random.Range(0, eventPrefabs.Length)]).GetComponent<EventBase>();
    }

    public virtual void WorldActivate()
    {
        gameObject.transform.position = new Vector2(0, 1.07f);
    }

    public virtual void WorldInactivate()
    {
        gameObject.transform.position = Vector2.one * 100;
    }

    public void EventDestroyed(int index)
    {
        isEventCreated[index] = false;
    }

    public int GetTownLevelSum()
    {
        int sum = 0;
        for(int i = 0;i<towns.Length;i++)
        {
            sum += towns[i].level;
        }
        return sum;
    }

    public int GetWorldEventCount()
    {
        int count = 0;
        for(int i =0;i<towns.Length;i++)
        {
            if (isEventCreated[i])
                count++;
        }
        return count;
    }
}