using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    Tile[] tiles;
    public Image[] coolTimeImages;
    static Sprite buttonDownSprite;
    static Sprite buttonUpSprite;

    void Awake()
    {
        List<Tile> tileList = new List<Tile>();
        for (int i = 1; i < 6; i++)
        {
            tileList.Add(GameObject.Find("Command" + i.ToString()).GetComponent<Tile>());
        }
        tiles = tileList.ToArray();

        if (buttonDownSprite == null || buttonUpSprite == null)
        {
            buttonUpSprite = Resources.Load<Sprite>("UI/TileButton");
            buttonDownSprite = Resources.Load<Sprite>("UI/TileButtonDown");
        }
    }

    void Update()
    {

    }
    
    public void ShowCoolTime(Tile tile)
    {
        StartCoroutine(CoolTimeProcess(tile));
    }

    IEnumerator CoolTimeProcess(Tile tile)
    {
        Image targetImage = coolTimeImages[tile.ID - 1];
        targetImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while(true)
        {
            elapsedTime += Time.deltaTime;
            targetImage.fillAmount = elapsedTime / tile.clearTime;
            if (elapsedTime > tile.clearTime)
                break;
            yield return null;
        }
        targetImage.gameObject.SetActive(false);
    }

    public void ResetTile(int ID)
    {
        Tile targetTile = null;
        foreach (Tile eachTile in tiles)
        {
            if (eachTile.ID == ID)
                targetTile = eachTile;
        }
        targetTile.isAttatched = false;
        targetTile.GetComponent<BoxCollider2D>().enabled = true;
        targetTile.GetComponent<DragAndDrop>().ResetPosition();
    }
}