using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Floor : MonoBehaviour
{
    private int index;
    public Tilemap baseTilemap;
    public Tilemap wallTilemap;
    public Tilemap[] stairUpTilemap;
    public Tilemap[] stairDownTilemap;
    public Tilemap switchTilemap;

    public void SetupTilemaps()
    {
        TileBase baseTile = Resources.Load<TileBase>("Base");
        baseTilemap.SetTile(new Vector3Int(0, 0, 0), baseTile);

        TileBase wallTile = Resources.Load<TileBase>("Wall");
        wallTilemap.SetTile(new Vector3Int(0, 0, 0), wallTile);

        index = 1;
        foreach (Tilemap stairUp in stairUpTilemap)
        {
            
            string str = "StairUp" + index;
            TileBase stairUpTile = Resources.Load<TileBase>(str);
            stairUp.SetTile(new Vector3Int(0, 0, 0), stairUpTile);
            index++;
        }

        index = 1;
        foreach (Tilemap stairDown in stairDownTilemap)
        {
            string str = "StairDown" + index;
            TileBase stairDownTile = Resources.Load<TileBase>(str);
            stairDown.SetTile(new Vector3Int(0, 0, 0), stairDownTile);
            index++;
        }

        TileBase switchTile = Resources.Load<TileBase>("Switch");
        switchTilemap.SetTile(new Vector3Int(0, 0, 0), switchTile);
    }
    // Function to clear the tilemaps
    public void ClearTilemaps()
    {
        baseTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        foreach (Tilemap stairUp in stairUpTilemap)
        {
            stairUp.ClearAllTiles();
        }
        foreach (Tilemap stairDown in stairDownTilemap)
        {
            stairDown.ClearAllTiles();
        }
        if(switchTilemap != null){
            switchTilemap.ClearAllTiles();
        }
    }

    
}
