using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    Tile[] tiles;

    //SpriteRenderer[] tileButtonRenderers;

    static Sprite buttonDownSprite;
    static Sprite buttonUpSprite;

    void Awake()
    {
        List<Tile> tileList = new List<Tile>();
        //List<SpriteRenderer> buttonRendererList = new List<SpriteRenderer>();
        for (int i = 1; i < 6; i++)
        {
            tileList.Add(GameObject.Find("Command" + i.ToString()).GetComponent<Tile>());
            //buttonRendererList.Add(GameObject.Find("TileButton" + i.ToString()).GetComponent<SpriteRenderer>());
        }
        tiles = tileList.ToArray();
        //tileButtonRenderers = buttonRendererList.ToArray();

        if (buttonDownSprite == null || buttonUpSprite == null)
        {
            buttonUpSprite = Resources.Load<Sprite>("UI/TileButton");
            buttonDownSprite = Resources.Load<Sprite>("UI/TileButtonDown");
        }
    }

    void Update()
    {

    }

    public void TileAttatched(int ID)
    {
        //tileButtonRenderers[ID - 1].sprite = buttonDownSprite;
    }

    public void ResetTile(int ID)
    {
        Tile targetTile = null;
        foreach (Tile eachTile in tiles)
        {
            if (eachTile.ID == ID)
                targetTile = eachTile;
        }
        targetTile.GetComponent<DragAndDrop>().ResetPosition();
        //tileButtonRenderers[ID - 1].sprite = buttonUpSprite;
    }
}