using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public struct CreateInterval
{
    public CreateInterval(int startLevel, int endLevel, float min, float max)
    {
        this.startLevel = startLevel;
        this.endLevel = endLevel;

        this.min = min;
        this.max = max;
    }
    public int startLevel;
    public int endLevel;

    public float min;
    public float max;
}
public class DataManager : MonoBehaviour
{
    public TextAsset dataFileJson;

    CreateInterval[] createIntervals;
    int[] townLevelUpgradeValues = new int[4];
    public int[] eventSuccessFollower = new int[5];
    public int[] eventFailFollower = new int[5];
    void Awake()
    {
        LoadFile();
    }

    void LoadFile()
    {
        JsonData dataFile = JsonMapper.ToObject(dataFileJson.text);

        List<int> upgradeValueList = new List<int>();
        for (int i = 1; i < 5; i++)
        {
            upgradeValueList.Add(int.Parse(dataFile["Town Level Upgrade"]["Level " + i.ToString()].ToString()));
        }
        townLevelUpgradeValues = upgradeValueList.ToArray();

        List<CreateInterval> createIntervalList = new List<CreateInterval>();
        int length = dataFile["Event Create Intervals"].Count;
        for (int i = 0; i < length; i++)
        {
            CreateInterval newInterval = new CreateInterval();
            newInterval.startLevel = int.Parse(dataFile["Event Create Intervals"][i]["Start Level"].ToString());
            newInterval.endLevel = int.Parse(dataFile["Event Create Intervals"][i]["End Level"].ToString());
            newInterval.min = float.Parse(dataFile["Event Create Intervals"][i]["Min"].ToString());
            newInterval.max = float.Parse(dataFile["Event Create Intervals"][i]["Max"].ToString());
            createIntervalList.Add(newInterval);
        }
        createIntervals = createIntervalList.ToArray();

        List<int> successFollowerList = new List<int>();
        List<int> failFollowerList = new List<int>();
        for(int i = 0;i<5;i++)
        {
            successFollowerList.Add(int.Parse(dataFile["Event Success Follower"][(i + 1).ToString()].ToString()));
            failFollowerList.Add(int.Parse(dataFile["Event Fail Follower"][(i + 1).ToString()].ToString()));
        }
        eventSuccessFollower = successFollowerList.ToArray();
        eventFailFollower = failFollowerList.ToArray();
    }

    public CreateInterval GetCreateInterval(int levelSum)
    {
        for (int i = 0; i < createIntervals.Length; i++)
        {
            if (createIntervals[i].startLevel <= levelSum && createIntervals[i].endLevel >= levelSum)
            {
                return createIntervals[i];
            }
        }
        
        throw new System.ArgumentException("Level Sum : " + levelSum.ToString());
    }

    public int GetTownLevelUpPoint(int level)
    {
        return townLevelUpgradeValues[level];
    }
   
}