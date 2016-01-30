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

    int approvalRating = 100;
    public bool isRun = true;

    int clearedEventCount = 0;
    int failedEventCount = 0;
    int goatGetCount = 0;

    public Text gameOverClearedEventText;
    public Text gameOverFailedEventText;
    public Text gameOverGoatGetText;

    void Awake()
    {
        SetApprovalRatingImage();
    }

    void Start()
    {
        StartCoroutine(SceneFader.Instance.FadeIn(1f));
    }
    public void EventCleared()
    {
        clearedEventCount++;
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
        Debug.Break();
        goatGetCount++;
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

    }
}