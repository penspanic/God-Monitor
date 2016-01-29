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

    WorldManager worldMgr;

    void Awake()
    {
        worldMgr = GameObject.FindObjectOfType<WorldManager>();
    }
    void Update()
    {
        
    }

    public void WorldChanged()
    {
        //for(int i = 0;i<5;i++)
        //{
        //    world
        //}
    }
}
