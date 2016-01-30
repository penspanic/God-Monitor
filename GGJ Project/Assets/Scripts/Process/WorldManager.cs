using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    public WorldBase[] worlds;
    public Button leftButton;
    public Button rightButton;

    GameManager gameMgr;
    DataManager dataMgr;
    WorldBase currWorld;
    int currWorldIndex;

    void Awake()
    {
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        dataMgr = GameObject.FindObjectOfType<DataManager>();
        currWorld = worlds[currWorldIndex];

        foreach (WorldBase eachWorld in worlds)
        {
            eachWorld.WorldInactivate();
        }
        currWorld.WorldActivate();
        leftButton.interactable = false;

        StartCoroutine(EventCreateProcess());
    }

    IEnumerator EventCreateProcess()
    {
        float nextWaitTime = 1;
        while (gameMgr.isRun)
        {
            yield return new WaitForSeconds(nextWaitTime);
            GetNextEventCreateWorld().CreateEvent();
            CreateInterval interval = dataMgr.GetCreateInterval(GetAllTownLevelSum());
            Debug.Log(GetAllTownLevelSum());
            nextWaitTime = Random.Range(interval.min, interval.max);
        }
    }

    WorldBase GetNextEventCreateWorld()
    {
        WorldBase world = null;
        while (true)
        {
            world = worlds[Random.Range(0, 5)];
            if (world.IsEmptyPlaceExist())
                break;
        }
        return world;
    }

    public int GetAllTownLevelSum()
    {
        int sum = 0;
        for (int i = 0; i < worlds.Length; i++)
        {
            sum += worlds[i].GetTownLevelSum();
        }
        return sum;
    }

    public void OnLeftButtonDown()
    {
        if (currWorldIndex == 0)
            return;
        currWorld.WorldInactivate();
        currWorldIndex--;
        currWorld = worlds[currWorldIndex];
        currWorld.WorldActivate();

        rightButton.interactable = true;
        if (currWorldIndex == 0)
        {
            leftButton.interactable = false;
        }
    }

    public void OnRightButtonDown()
    {
        if (currWorldIndex == worlds.Length - 1)
            return;
        currWorld.WorldInactivate();
        currWorldIndex++;
        currWorld = worlds[currWorldIndex];
        currWorld.WorldActivate();

        leftButton.interactable = true;
        if (currWorldIndex == worlds.Length - 1)
        {
            rightButton.interactable = false;
        }
    }

    public int GetCurrWorldIndex()
    {
        return currWorldIndex;
    }
}