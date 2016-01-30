using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Image approvalRatingImage;
    const int approvalRatingLosePoint = 5;
    const int approvalRatingIncreasePoint = 5;

    int approvalRating = 100;
    public bool isRun = true;
    void Awake()
    {
        SetApprovalRatingImage();
    }

    public void EventCleared()
    {
        approvalRating += approvalRatingIncreasePoint;
        if (approvalRating > 100)
            approvalRating = 100;
        SetApprovalRatingImage();
    }

    public void EventDestroyed()
    {
        approvalRating -= approvalRatingLosePoint;
        SetApprovalRatingImage();
        if (approvalRating < 0)
        {
            GameOver();
        }
    }

    void SetApprovalRatingImage()
    {
        approvalRatingImage.fillAmount = (float)approvalRating / (float)100;
    }

    void GameOver()
    {
        isRun = false;
    }

    void GameClear()
    {

    }
}