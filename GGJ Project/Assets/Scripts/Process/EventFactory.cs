using UnityEngine;
using System.Collections;

public class EventFactory : MonoBehaviour
{
    public GameObject[] eventPrefabs;

    void Awake()
    {

    }

    public EventBase CreateEvent(Transform targetTransform)
    {
        int index = Random.Range(0, eventPrefabs.Length);

        EventBase currEvent = Instantiate<GameObject>(eventPrefabs[index]).GetComponent<EventBase>();

        currEvent.transform.SetParent(targetTransform);
        currEvent.transform.localPosition = Vector2.zero;
        return currEvent;
    }
}