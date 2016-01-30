using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    public World[] worlds;

    public Sprite normalButtonSprite;
    public Sprite leftDownSprite;
    public Sprite rightDownSprite;

    public SpriteRenderer channelButtonRenderer;

    GameManager gameMgr;
    DataManager dataMgr;
    World currWorld;
    int currWorldIndex = 0;

    public AudioSource buttonSoundSource;

    public AudioClip buttonClickClip;
    public AudioClip buttonReleaseClip;

    public GameObject noise;
    public 
    void Awake()
    {
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        dataMgr = GameObject.FindObjectOfType<DataManager>();
        currWorld = worlds[currWorldIndex];

        foreach (World eachWorld in worlds)
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
            nextWaitTime = Random.Range(interval.min, interval.max);
        }
    }

    World GetNextEventCreateWorld()
    {
        World world = null;
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
        ShowNoise();
        buttonSoundSource.PlayOneShot(buttonClickClip);
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
        ShowNoise();
        buttonSoundSource.PlayOneShot(buttonClickClip);
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
        buttonSoundSource.PlayOneShot(buttonReleaseClip);
        channelButtonRenderer.sprite = normalButtonSprite;
    }

    public void OnRightButtonUp()
    {
        buttonSoundSource.PlayOneShot(buttonReleaseClip);
        channelButtonRenderer.sprite = normalButtonSprite;
    }

    void ShowNoise()
    {
        StartCoroutine(NoiseProcess());
    }

    IEnumerator NoiseProcess()
    {
        noise.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        noise.SetActive(false);
    }

    public int GetCurrWorldIndex()
    {
        return currWorldIndex;
    }
}