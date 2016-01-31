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

    public GameObject Smoke
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
    static GameObject message;


    GameObject levelUpParticle;
    void Awake()
    {
        level = 1;
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        dataMgr = GameObject.FindObjectOfType<DataManager>();
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(1);
        levelUpParticle = Resources.Load<GameObject>("Prefabs/ParticleSystems/TownLevelUpParticles");
        if (townSprites == null)
        {
            townSprites = Resources.LoadAll<Sprite>("Town");
            successClip = Resources.Load<AudioClip>("Sounds/ritual_success_01");
            failClip = Resources.Load<AudioClip>("Sounds/ritual_fail_01");
            message = Resources.Load<GameObject>("Prefabs/Message");
        }
        sprRenderer.sprite = townSprites[0];

        Smoke = transform.GetChild(0).gameObject;
        Smoke.transform.localPosition = new Vector2(0, -0.3f);
    }

    public bool GetEventNeedSuccess()
    {
        if (GetComponentInChildren<EventBase>() != null)
        {
            if (GetComponentInChildren<EventBase>().currTile == null)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void ShowMessage(EventBase currEvent)
    {
        transform.localScale = Vector3.one;
        StartCoroutine(MessageScaleChange(currEvent));
        if (Smoke.activeSelf)
        {
            currEvent.transform.localPosition = new Vector2(-0.4f, 0.7f);
        }
        else
        {
            currEvent.transform.localPosition = new Vector2(0, 0.7f);
        }
    }

    IEnumerator MessageScaleChange(EventBase currEvent)
    {
        float existTime = currEvent.existTime;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(existTime / 2);
        existTime /= 2;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= existTime)
                break;
            if (!currEvent.isWaiting)
                break;
            if (currEvent == null)
                break;
            currEvent.transform.localScale = new Vector2(1.5f + Mathf.Sin(elapsedTime * 3) / 7, 1.5f + Mathf.Sin(elapsedTime * 3) / 7);

            yield return null;
        }
    }

    public void EventCleared()
    {
        if (!gameMgr.isRun)
            return;
        gameMgr.Follower += dataMgr.eventSuccessFollower[level - 1];
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
        if (!gameMgr.isRun)
            return;
        gameMgr.Follower -= dataMgr.eventFailFollower[level - 1];
        audioSource.PlayOneShot(failClip);
        clearedEventStreak = 0;
        Smoke.SetActive(false);
    }

    public bool GoatActivationCheck()
    {
        float rand = Random.Range(0f, 1f);
        if (rand < goatChance)
        {
            Smoke.SetActive(true);
            clearedEventStreak = 0;
        }

        return Smoke.activeSelf;
    }

    public void GoatChanceLogic()
    {
        if (Smoke.activeSelf)
        {
            Smoke.SetActive(false);

            gameMgr.GoatReceived();
        }

        clearedEventStreak++;
        if (clearedEventStreak >= 3)
            goatChance = .5f;
        else if (clearedEventStreak >= 4)
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
        particleObj = Instantiate(levelUpParticle);
        particleObj.transform.SetParent(this.transform);
        particleObj.transform.position = this.transform.position;
        StartCoroutine(ParticleDestroy());
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(level - 1);
        sprRenderer.sprite = townSprites[level - 1];
        transform.Translate(0, 0.1f, 0);
    }

    GameObject particleObj;

    IEnumerator ParticleDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(particleObj);
    }
}