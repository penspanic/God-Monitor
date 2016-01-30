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
    GameObject messageObj;
    int eventClearCount = 0;
    int nextLevelUpPoint;
    int clearedEventStreak;
    float goatChance;

    AudioSource audioSource;
    static AudioClip successClip;
    static AudioClip failClip;
    static GameObject message;

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
            message = Resources.Load<GameObject>("Prefabs/Message");
        }
        sprRenderer.sprite = townSprites[0];

        messageObj = Instantiate<GameObject>(message);
        messageObj.transform.SetParent(this.transform);
        messageObj.transform.localPosition = new Vector2(0, 0.5f);
        messageObj.SetActive(false);
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
        messageObj.SetActive(true);
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
        Vector2 startScale = transform.localScale;

        yield return new WaitForSeconds(existTime / 2);
        existTime /= 2;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= existTime)
                break;
            for (int i = 0; i < transform.childCount; i++)
            {
                messageObj.transform.localScale = new Vector2(1 + Mathf.Sin(elapsedTime * 3) / 7, 1 + Mathf.Sin(elapsedTime * 3) / 7);
            }

            yield return null;
        }
    }

    public void EventCleared()
    {
        gameMgr.follower += dataMgr.eventSuccessFollower[level - 1];
        messageObj.SetActive(false);
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
        gameMgr.follower -= dataMgr.eventFailFollower[level - 1];
        messageObj.SetActive(false);
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
        if (clearedEventStreak >= 0)
            goatChance = .9f;
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
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(level - 1);
        sprRenderer.sprite = townSprites[level - 1];
    }
}
