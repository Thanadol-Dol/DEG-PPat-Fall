using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Floor : MonoBehaviour
{
    public Tilemap baseTilemap;
    public Tilemap wallTilemap;

    // Function to set up the tilemaps
    public void SetupTilemaps()
    {
        // Example: Place a base tile at the center
        TileBase baseTile = Resources.Load<TileBase>("Base");
        baseTilemap.SetTile(new Vector3Int(0, 0, 0), baseTile);

        // Example: Place a wall tile at the center
        TileBase wallTile = Resources.Load<TileBase>("Wall");
        wallTilemap.SetTile(new Vector3Int(0, 0, 0), wallTile);
    }

    // Function to clear the tilemaps
    public void ClearTilemaps()
    {
        baseTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    
}
