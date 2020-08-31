using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public static TileManager instance;
    public Tilemap Tilemap;
    public Dictionary<Vector3Int, WorldTile> tiles;

    private void Start() {
        populateWorldTiles();
    }

    public Dictionary<Vector3Int, WorldTile> getTilesDictionary() {
        populateWorldTiles();
        return tiles;
    }

    //Instantiates our dictionary --> Adds every tile in our grid to it as a WorldTile object 
    private void populateWorldTiles() {
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
