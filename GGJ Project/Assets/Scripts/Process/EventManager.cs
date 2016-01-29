using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public Transform[] eventCreateTransform;
    
    bool[] isEventCreated;

    
    void Awake()
    {
        isEventCreated = new bool[eventCreateTransform.Length];

        StartCoroutine(EventCreateProcess());
    }
	void Update () 
    {
	
	}

    IEnumerator EventCreateProcess()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            CreateEvent();
        }
    }

    void CreateEvent()
    {
        int createIndex = 0;
        while(true)
        {
            createIndex = Random.Range(0, eventCreateTransform.Length);
            if (isEventCreated[createIndex])
                continue;
            else
                break;
                
        }
        isEventCreated[createIndex] = true;

        EventFactory.CreateEvent().SetEvent();
    }
}
