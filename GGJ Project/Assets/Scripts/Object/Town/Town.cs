using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour
{
    public int level
    {
        get;
        private set;
    }

    int eventClearCount;
    void Awake()
    {
        level = 1;
    }

    public void EventCleared()
    {
        eventClearCount++;
    }
}
