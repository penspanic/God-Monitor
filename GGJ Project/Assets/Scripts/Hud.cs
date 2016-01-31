using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField]
    Text score;
    [SerializeField]
    Text time;

	GameManager gameMgr;
    double seconds;

	void Start()
    {
        gameMgr = FindObjectOfType<GameManager>();
        gameMgr.onScoreChanged += SetScoreLbl;
        StartCoroutine(Timer());
	}
	
    void SetScoreLbl(int val)
    {
        score.text =  val.ToString();
    }

    IEnumerator Timer()
    {
        while(gameMgr.isRun)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            System.TimeSpan playTime = System.TimeSpan.FromSeconds(seconds);
            //string s = playTime.Minutes.ToString() + " : " + playTime.Seconds.ToString();
            string s = string.Format("{0:D2}:{1:D2}", playTime.Minutes, playTime.Seconds);
            time.text = s;
        }
    }
}
