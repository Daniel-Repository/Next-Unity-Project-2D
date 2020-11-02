using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public static TileManager instance;
    public Tilemap Tilemap;
    public Dictionary<Vector3Int, WorldTile> tiles;
    private WorldTile worldTiles;

    //Tiles
    public TileBase highlightGroundTile;
    public TileBase groundTile;
    public TileBase tilledGround;

    private void Start() {
        tiles = new Dictionary<Vector3Int, WorldTile>();
        populateWorldTiles();
    }

    public Dictionary<Vector3Int, WorldTile> getTilesDictionary() {
        populateWorldTiles();
        return tiles;
    }

    public void timerUpdate() {
        foreach (KeyValuePair<Vector3Int, WorldTile> entry in tiles) {
            if(entry.Value.TileBase == tilledGround) {
                updateWorldTiles(entry.Key, "TileState", entry.Value.TileBase);
            }
        }
    }

    //Instantiates our dictionary --> Adds every new tile from grid to it as a WorldTile object 
    private void populateWorldTiles() {

        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin) {

            Vector3Int gridPos = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(gridPos)) continue;
            if (tiles.ContainsKey(gridPos)) break;

            var tile = new WorldTile {
                TileGridPos = gridPos,
                TileBase = Tilemap.GetTile(gridPos),
                TilemapMember = Tilemap,
                TileState = 0
            };

            tiles.Add(tile.TileGridPos, tile);
        }
    } 

    public void updateWorldTiles(Vector3Int gridPos, string state, TileBase tb) {
        switch (state) {

            case "TileBase":
                if (tiles.TryGetValue(gridPos, out worldTiles)) {
                    tiles[gridPos].TileBase = tb;
                }
                break;

            case "TileState":
                if(tiles.TryGetValue(gridPos, out worldTiles)) {
                    tiles[gridPos].TileState += 1;
                }
                break;

            default:
                break;
        }
    }

}
