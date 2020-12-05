using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public static TileManager instance;
    private Tilemap tilemapGround;
    public Dictionary<Vector3Int, WorldTile> tiles;
    private WorldTile worldTiles;

    //Tiles
    public TileBase weedsGround;
    public TileBase grassGround;
    public TileBase basicGround;
    public TileBase tilledGround;
    public TileBase pinkSeed;
    public TileBase pinkSprout;
    public TileBase pinkFlower;
    public TileBase blueSeed;
    public TileBase blueSprout;
    public TileBase blueFlower;
    public TileBase yellowSeed;
    public TileBase yellowSprout;
    public TileBase yellowFlower;

    private List<TileBase> listSeeds;
    private List<TileBase> listSprouts;
    private List<TileBase> listFlowers;
    

    //Counters
    private int tilledDeprecation = 120;
    private int grassGrowth = 120;
    private int weedsGrowth = 120;
    private int sproutGrowth = 120;
    private int flowerGrowth = 120;

    private void Start() {
        tilemapGround = GameObject.Find("TileMap_GroundPieces").GetComponent<Tilemap>();
        tiles = new Dictionary<Vector3Int, WorldTile>();
        populateWorldTiles();

        listSeeds = new List<TileBase>();
        listSeeds.AddRange(new List<TileBase> { pinkSeed, blueSeed, yellowSeed });

        listSprouts = new List<TileBase>();
        listSprouts.AddRange(new List<TileBase> { pinkSprout, blueSprout, yellowSprout });

        listFlowers = new List<TileBase>();
        listFlowers.AddRange(new List<TileBase> { pinkFlower, blueFlower, yellowFlower });
    }

    public Dictionary<Vector3Int, WorldTile> getTilesDictionary() {
        populateWorldTiles();
        return tiles;
    }

    //Update the state of each tile once the timer ends
    public void timerUpdate() {
        foreach (KeyValuePair<Vector3Int, WorldTile> entry in tiles) {
            updateWorldTiles(entry.Key, "TileState", entry.Value.TileBase);
        }
    }

    //Adds our initial tiles to our dictionary
    private void populateWorldTiles() {

        foreach (Vector3Int pos in tilemapGround.cellBounds.allPositionsWithin) {

            Vector3Int gridPos = new Vector3Int(pos.x, pos.y, pos.z);

            if (!tilemapGround.HasTile(gridPos)) continue;
            if (tiles.ContainsKey(gridPos)) break;

            var tile = new WorldTile {
                TileGridPos = gridPos,
                TileBase = tilemapGround.GetTile(gridPos),
                TilemapMember = tilemapGround,
                TileState = 0
            };

            tiles.Add(tile.TileGridPos, tile);
        }
    } 

    //Add new tile to dictionary
    public void addWorldTiles(Vector3Int gridPos) {
        if (tiles.ContainsKey(gridPos)) {
            //Do nothing, as there's already a tile here (should throw erorr but YOLO)
        } 
        else {
            var tile = new WorldTile {
                TileGridPos = gridPos,
                TileBase = tilemapGround.GetTile(gridPos),
                TilemapMember = tilemapGround,
                TileState = 0
            };

            tiles.Add(tile.TileGridPos, tile);
        }
    }
    
    //Update our tiles
    public void updateWorldTiles(Vector3Int gridPos, string state, TileBase tb) {
        switch (state) {

            case "TileBase":
                if (tiles.TryGetValue(gridPos, out worldTiles)) {
                    tiles[gridPos].TileBase = tb;
                    tiles[gridPos].TileState = 0;
                }
                break;

            case "TileState":
                if(tiles.TryGetValue(gridPos, out worldTiles)) {
                    tiles[gridPos].TileState += 1;

                    //Sprout --> Flower
                    if (listSprouts.Contains(tiles[gridPos].TileBase) && tiles[gridPos].TileState == flowerGrowth) {
                        TileBase flower = getNextGrowth(tiles[gridPos].TileBase);
                        tiles[gridPos].TileBase = flower;
                        tilemapGround.SetTile(gridPos, flower);
                        tiles[gridPos].TileState = 0;
                    }

                    //Seed --> Sprout
                    if (listSeeds.Contains(tiles[gridPos].TileBase) && tiles[gridPos].TileState == sproutGrowth) {
                        TileBase sprout = getNextGrowth(tiles[gridPos].TileBase);
                        tiles[gridPos].TileBase = sprout;
                        tilemapGround.SetTile(gridPos, sprout);
                        tiles[gridPos].TileState = 0;
                    }

                    //Tilled --> Basic Ground
                    if (tiles[gridPos].TileBase == tilledGround && tiles[gridPos].TileState == tilledDeprecation) {
                        tiles[gridPos].TileBase = basicGround;
                        tilemapGround.SetTile(gridPos, basicGround);
                        tiles[gridPos].TileState = 0;
                    }

                    //Basic Goround --> Grass Ground
                    if (tiles[gridPos].TileBase == basicGround && tiles[gridPos].TileState == grassGrowth) {
                        tiles[gridPos].TileBase = grassGround;
                        tilemapGround.SetTile(gridPos, grassGround);
                        tiles[gridPos].TileState = 0;
                    }

                    //Grass Ground --> Weeds Ground
                    if (tiles[gridPos].TileBase == grassGround && tiles[gridPos].TileState == weedsGrowth) {
                        tiles[gridPos].TileBase = weedsGround;
                        tilemapGround.SetTile(gridPos, weedsGround);
                        tiles[gridPos].TileState = 0;
                    }
                }
                break;

            default:
                break;
        }
    }

    //Retrun the next growth stage depending on flower type
    private TileBase getNextGrowth(TileBase tb) {
        if(tb == pinkSeed) {
            return pinkSprout;
        }
        else if(tb == pinkSprout) {
            return pinkFlower;
        }
        else if (tb == blueSeed) {
            return blueSprout;
        }
        else if (tb == blueSprout) {
            return blueFlower;
        }
        else if (tb == yellowSeed) {
            return yellowSprout;
        }
        else if (tb == yellowSprout) {
            return yellowFlower;
        }
        else {
            return tb;
        }
    }

}
