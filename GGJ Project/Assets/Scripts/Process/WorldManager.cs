using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    public WorldBase[] worlds;

    public Sprite normalButtonSprite;
    public Sprite leftDownSprite;
    public Sprite rightDownSprite;

    public SpriteRenderer channelButtonRenderer;

    GameManager gameMgr;
    DataManager dataMgr;
    WorldBase currWorld;
    int currWorldIndex = 0;

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
        channelButtonRenderer.sprite = leftDownSprite;
        currWorld.WorldInactivate();
        if (currWorldIndex == 0)
            currWorldIndex = 4;
        else
            currWorldIndex--;
        currWorld = worlds[currWorldIndex];
        currWorld.WorldActivate();

    }

    public void OnRightButtonDown()
    {
        channelButtonRenderer.sprite = rightDownSprite;
        currWorld.WorldInactivate();
        if (currWorldIndex == 4)
            currWorldIndex = 0;
        else
            currWorldIndex++;
        currWorld = worlds[currWorldIndex];
        currWorld.WorldActivate();

    }

    public void OnLeftButtonUp()
    {
        channelButtonRenderer.sprite = normalButtonSprite;
    }

    public void OnRightButtonUp()
    {
        channelButtonRenderer.sprite = normalButtonSprite;
    }

    public int GetCurrWorldIndex()
    {
        return currWorldIndex;
    }
}