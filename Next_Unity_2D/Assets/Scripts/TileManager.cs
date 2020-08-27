using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public static TileManager instance;
    public Tilemap Tilemap;
    public Dictionary<Vector3Int, WorldTile> tiles;

    //Called when we create an instance of this class
    private void Awake() {

        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        GetWorldTiles();
    }

    //Instantiates our dictionary --> Adds every tile in our grid to it as a WorldTile object 
    private void GetWorldTiles() {
        tiles = new Dictionary<Vector3Int, WorldTile>();

        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin) {

            Vector3Int gridPos = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(gridPos)) continue;
            var tile = new WorldTile {
                TileGridPos = gridPos,
                TileBase = Tilemap.GetTile(gridPos),
                TilemapMember = Tilemap
            };

            tiles.Add(tile.TileGridPos, tile);
        }
    } 
    
}
