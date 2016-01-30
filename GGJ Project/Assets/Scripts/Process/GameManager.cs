using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text approvalRatingText;
    const int approvalRatingLosePoint = 5;
    const int approvalRatingIncreasePoint = 5;

    int approvalRating = 100;
    public bool isRun = true;
    void Awake()
    {
        SetApprovalRatingText();
    }

    public void EventCleared()
    {
        approvalRating += approvalRatingIncreasePoint;
        SetApprovalRatingText();
    }

    public void EventDestroyed()
    {
        approvalRating -= approvalRatingLosePoint;
        SetApprovalRatingText();
        if (approvalRating < 0)
        {
            GameOver();
        }
    }

    void SetApprovalRatingText()
    {
        approvalRatingText.text = approvalRating.ToString() + "/" + "100";
    }

    void GameOver()
    {
        isRun = false;
    }

    void GameClear()
    {

    }
}