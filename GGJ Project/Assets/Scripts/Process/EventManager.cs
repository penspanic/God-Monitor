using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    public Transform[] eventCreateTransform;

    public bool[] isEventCreated;

    EventFactory eventFactory;
    void Awake()
    {
        isEventCreated = new bool[eventCreateTransform.Length];
        eventFactory = GameObject.FindObjectOfType<EventFactory>();
        StartCoroutine(EventCreateProcess());
    }
    void Update()
    {

    }

    IEnumerator EventCreateProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            CreateEvent();
        }
    }

    void CreateEvent()
    {
        int createIndex = 0;
        bool isEmptyPlaceExist = false;
        for (int i = 0; i < isEventCreated.Length; i++)
        {
            if (!isEventCreated[i])
                isEmptyPlaceExist = true;
        }
        if (!isEmptyPlaceExist)
            return;
        while (true)
        {
            createIndex = Random.Range(0, eventCreateTransform.Length);
            if (isEventCreated[createIndex])
                continue;
            else
                break;

        }
        isEventCreated[createIndex] = true;

        eventFactory.CreateEvent(eventCreateTransform[createIndex]).SetEvent(createIndex);
    }
}
