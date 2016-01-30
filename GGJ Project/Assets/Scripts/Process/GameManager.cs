using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Image approvalRatingImage;
    public GameObject gameOver;
    public GameObject gameClear;

    const int approvalRatingLosePoint = 50;
    const int approvalRatingIncreasePoint = 5;

    WorldManager worldMgr;
    int approvalRating = 100;
    public bool isRun = true;

    int clearedEventCount = 0;
    int failedEventCount = 0;
    int goatGetCount = 0;

    public Text gameOverClearedEventText;
    public Text gameOverFailedEventText;
    public Text gameOverGoatGetText;

    public AudioSource teleportSource;
    public AudioClip teleportSound;
    public AudioClip dropSound;

    public GameObject teleport;
    public GameObject goatPrefab;
    void Awake()
    {
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
        goatGetCount++;
        GameObject goat = Instantiate<GameObject>(goatPrefab);
        goat.transform.position = new Vector2(5.09f, 2.56f);
        teleport.SetActive(true);
        teleport.GetComponent<Animator>().Play("Teleport_Appear");
        teleportSource.PlayOneShot(teleportSound);
    }

    public void ReplayGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void SetApprovalRatingImage()
    {
        approvalRatingImage.fillAmount = (float)approvalRating / (float)100;
    }

    void GameOver()
    {
        isRun = false;
        gameOver.SetActive(true);
        gameOverClearedEventText.text = clearedEventCount.ToString();
        gameOverFailedEventText.text = failedEventCount.ToString();
        gameOverGoatGetText.text = goatGetCount.ToString();
    }

    void GameClear()
    {
        gameClear.SetActive(true);
    }
}