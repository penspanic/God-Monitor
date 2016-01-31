using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public System.Action<int> onFollowerChanged;

    public Image approvalRatingImage;
    public GameObject gameOver;
    public GameObject gameClear;

    const int approvalRatingLosePoint = 10;
    const int approvalRatingIncreasePoint = 5;

    WorldManager worldMgr;
    int approvalRating = 100;
    public bool isRun = true;

    public int clearedEventCount = 0;

    int failedEventCount = 0;
    int goatGetCount = 0;

    int follower = 0;

    public int Follower
    {
        set
        {
            follower = value;
            if (onFollowerChanged != null)
                onFollowerChanged(follower);
        }
        get { return follower; }
    }

    public bool isPaused
    {
        get;
        private set;
    }

    public Text gameOverFollowerText;
    public Text gameOverGoatGetText;

    public Text gameClearFollowerText;
    public Text gameClearGoatGetText;

    AudioSource bgmAudioSource;
    public AudioSource effectSoundSource;
    public AudioSource teleportSource;
    public AudioClip teleportSound;
    public AudioClip dropSound;
    public AudioClip gameOverSound;
    public AudioClip replaySound;

    public GameObject teleport;
    public GameObject goatPrefab;

    public GameObject pauseUI;
    public GameObject followerObj;

    void Awake()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
        SetApprovalRatingImage();
    }

    void Start()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
    }
    public void EventCleared()
    {
        clearedEventCount++;
        int levelSum = worldMgr.GetAllTownLevelSum();
        if (levelSum >= 75)
            GameClear();
        approvalRating += approvalRatingIncreasePoint;
        if (approvalRating > 100)
            approvalRating = 100;
        SetApprovalRatingImage();
    }

    public void EventDestroyed()
    {
        if (!isRun)
            return;

        failedEventCount++;
        approvalRating -= approvalRatingLosePoint;
        SetApprovalRatingImage();
        if (approvalRating <= 0)
        {
            GameOver();
        }
    }

    public void GoatReceived()
    {
        follower += 5000;
        goatGetCount++;
        GameObject goat = Instantiate<GameObject>(goatPrefab);
        goat.transform.position = new Vector2(5.09f, 2.56f);
        teleport.SetActive(true);
        teleport.GetComponent<Animator>().Play("Teleport_Appear");
        teleportSource.PlayOneShot(teleportSound);
    }

    public void ReplayGame()
    {
        effectSoundSource.PlayOneShot(replaySound);
        GameObject.FindObjectOfType<Canvas>().sortingLayerName = "Fade";
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "InGame"));
    }

    public void ExitGame()
    {
        GameObject.FindObjectOfType<Canvas>().sortingLayerName = "Fade";
        StartCoroutine(SceneFader.Instance.FadeOut(1f, "Title"));
        StartCoroutine(SceneFader.Instance.SoundFadeOut(1f, GameObject.FindObjectsOfType<AudioSource>()));
    }

    void SetApprovalRatingImage()
    {
        approvalRatingImage.fillAmount = (float)approvalRating / (float)100;
    }

    void GameOver()
    {
        isRun = false;
        isPaused = true;
        gameOver.SetActive(true);

        gameOverFollowerText.text = follower.ToString();
        gameOverGoatGetText.text = goatGetCount.ToString();

        Destroy(followerObj);
        Destroy(pauseUI);

        StartCoroutine(GameOverSoundProcess());


    }

    IEnumerator GameOverSoundProcess()
    {
        Debug.Log("fade Start");
        yield return StartCoroutine(SceneFader.Instance.SoundFadeOut(0.1f, new AudioSource[] { bgmAudioSource }));
        Debug.Log("fade End");

        bgmAudioSource.volume = 1;
        bgmAudioSource.clip = gameOverSound;
        bgmAudioSource.Play();
    }

    void GameClear()
    {
        isRun = false;
        isPaused = true;
        gameClear.SetActive(true);

        gameClearFollowerText.text = follower.ToString();
        gameClearGoatGetText.text = goatGetCount.ToString();

        Destroy(followerObj);
        Destroy(pauseUI);
    }
}