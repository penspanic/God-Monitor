using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour
{
    Sprite[] townSprites;
    public int level
    {
        get;
        private set;
    }

    DataManager dataMgr;
    SpriteRenderer sprRenderer;
    int eventClearCount = 0;
    int nextLevelUpPoint;
    void Awake()
    {
        level = 1;
        dataMgr = GameObject.FindObjectOfType<DataManager>();
        sprRenderer = GetComponent<SpriteRenderer>();
        nextLevelUpPoint = dataMgr.GetTownLevelUpPoint(1);
        townSprites = Resources.LoadAll<Sprite>("Town");
        sprRenderer.sprite = townSprites[0];
    }

    public void EventCleared()
    {
        if (level < 5)
        {
            eventClearCount++;
            if (eventClearCount >= nextLevelUpPoint)
                LevelUp();
        }
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
