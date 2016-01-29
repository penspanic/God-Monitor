using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    Tile[] tiles;

    void Awake()
    {
        List<Tile> tileList = new List<Tile>();
        for (int i = 1; i < 6; i++)
            tileList.Add(GameObject.Find("Command" + i.ToString()).GetComponent<Tile>());
        tiles = tileList.ToArray();
    }

    void Update()
    {

    }
   
    public void ResetTile(int ID)
    {
        Tile targetTile = null;
        foreach(Tile eachTile in tiles)
        {
            if (eachTile.ID == ID)
                targetTile = eachTile;
        }
        targetTile.GetComponent<DragAndDrop>().ResetPosition();
    }
}