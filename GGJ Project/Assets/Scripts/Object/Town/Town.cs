using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour
{
    static Sprite[] townSprites;
    public int level
    {
        get;
        private set;
    }

    public GameObject GoatIcon
    {
        get;
        private set;
    }

    GameManager gameMgr;
    DataManager dataMgr;
    SpriteRenderer sprRenderer;
    int eventClearCount = 0;
    int nextLevelUpPoint;
    int clearedEventStreak;
    float goatChance;

    AudioSource audioSource;
    static AudioClip successClip;
    static AudioClip failClip;

    void Awake()
    {
        level = 1;
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        dataMgr = GameObject.FindObjectOfType<DataManager>();
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(1);
        if (townSprites == null)
        {
            townSprites = Resources.LoadAll<Sprite>("Town");
            successClip = Resources.Load<AudioClip>("Sounds/ritual_success_01");
            failClip = Resources.Load<AudioClip>("Sounds/ritual_fail_01");
        }
        sprRenderer.sprite = townSprites[0];

        GoatIcon = transform.GetChild(0).gameObject;
    }

    public void EventCleared()
    {
        audioSource.PlayOneShot(successClip);
        GoatChanceLogic();

        if (level < 5)
        {
            eventClearCount++;
            if (eventClearCount >= nextLevelUpPoint)
                LevelUp();
        }
    }

    public void EventDestroyed()
    {
        audioSource.PlayOneShot(failClip);
        clearedEventStreak = 0;
        GoatIcon.SetActive(false);
    }

    public bool GoatActivationCheck()
    {
        float rand = Random.Range(0f, 1f);
        if( rand < goatChance )
        {
            GoatIcon.SetActive(true);
            clearedEventStreak = 0;
        }

        return GoatIcon.activeSelf;
    }

    public void GoatChanceLogic()
    {
        if( GoatIcon.activeSelf )
        {
            GoatIcon.SetActive(false);
            gameMgr.GoatReceived();
        }

        clearedEventStreak++;
        if(clearedEventStreak == 3)
            goatChance = .5f;
        else if(clearedEventStreak >= 4)
            goatChance = .6f;
        else
            goatChance = 0f;
    }

    void LevelUp()
    {
        level++;
        if (level == 5)
        {
            sprRenderer.sprite = townSprites[4];
            return;
        }
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(level - 1);
        sprRenderer.sprite = townSprites[level - 1];
    }
}
