using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lamp : MonoBehaviour
{
    public Image[] lampImages;
    public Sprite redSprite;
    public Sprite whiteSprite;
    public Sprite orangeSprite;
    public Sprite greenSprite;
    public Sprite blackSprite;

    WorldManager worldMgr;

    void Awake()
    {
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
    }
    void Update()
    {
        WorldChanged();
    }

    public void WorldChanged()
    {
        for (int i = 0; i < 5; i++)
        {
            int eventCount = worldMgr.worlds[i].GetWorldNeedSuccessCount();
            switch (eventCount)
            {
                case 0:
                    lampImages[i].sprite = blackSprite;
                    break;
                case 1:
                    lampImages[i].sprite = greenSprite;
                    break;
                case 2:
                    lampImages[i].sprite = orangeSprite;
                    break;
                case 3:
                    lampImages[i].sprite = redSprite;
                    break;
                default:

                    throw new System.Exception("Event count must less or equal then 3");
            }
            if (worldMgr.GetCurrWorldIndex() == i)
                lampImages[i].sprite = whiteSprite;
        }
    }
}
