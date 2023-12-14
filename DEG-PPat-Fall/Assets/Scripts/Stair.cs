using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stair : MonoBehaviour
{
    public Tilemap stairTilemap;

    // Function to set up the tilemaps
    public void SetupTilemaps()
    {
        TileBase stairTile = Resources.Load<TileBase>("Stair");
        stairTilemap.SetTile(new Vector3Int(0, 0, 0), stairTile);
    }

    // Function to clear the tilemaps
    public void ClearTilemaps()
    {
        stairTilemap.ClearAllTiles();
    }


}
