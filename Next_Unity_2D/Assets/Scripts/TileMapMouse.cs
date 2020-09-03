using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapMouse : MonoBehaviour {

    //Tilemaps
    private Tilemap tilemapGround;
    private Tilemap tilemapHighlight;

    //Tiles
    public TileBase highlightGroundTile;
    public TileBase groundTile;
    public TileBase tilledGround;

    //Other
    private WorldTile worldTiles;
    public TileManager tileManager;
    Vector3Int prevGridPos;
    bool highlightPlaced;
    private Dictionary<Vector3Int, WorldTile> tilesDict;

    // Start is called before the first frame update
    private void Start() {
        tilemapGround = GameObject.Find("TileMap_GroundPieces").GetComponent<Tilemap>();
        tilemapHighlight = GameObject.Find("TileMap_GroundPiecesHighlight").GetComponent<Tilemap>();
        tilesDict = tileManager.getTilesDictionary();
    }

    // Update is called once per frame
    void Update() {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemapGround.WorldToCell(mousePos);

        highlightPlaced = tilemapHighlight.HasTile(gridPos);

        //Clicking on a tile will use it's grid position to retrieve it's data from the dictionary
        if (Input.GetMouseButtonDown(0) && tilemapGround.HasTile(gridPos)) {
            if (tilesDict.TryGetValue(gridPos, out worldTiles)) {
                if (worldTiles.TileBase == groundTile) {
                    tilemapGround.SetTile(gridPos, tilledGround); //Till ground
                }
                print(worldTiles.TileGridPos);
            }
        }

        if (Input.GetKeyDown("space")) {
            addGroundTiles();
        }

        //Removing HL from previously hovered tile
        if (prevGridPos != null && prevGridPos != gridPos) {
            tilemapHighlight.SetTile(prevGridPos, null);
        }

        //Adding HL tile to currently hovered tile if current hover has tile and there's no HL placed.
        if (tilemapGround.HasTile(gridPos) && highlightPlaced == false) {
            tilemapHighlight.SetTile(gridPos, highlightGroundTile);
            prevGridPos = gridPos;
        } 
    }

    //Add next layer of groundtiles to our tilemap
    private void addGroundTiles() {
        
        var maxY = 0;
        var maxX = 0;
       
        //Calc max y and x values to add next group of tiles to
        foreach (KeyValuePair<Vector3Int, WorldTile> entry in tilesDict) {
            if (entry.Key.x <= maxX) {
                maxX -= 1;
            }
            if (entry.Key.y <= maxY) {
                maxY -= 1;
            }
        }

        //Increment through y values using maxX
        var y = 0;
        while (y >= maxY) {
            tilemapGround.SetTile(new Vector3Int(maxX, y, 0), groundTile);
            y -= 1;
        }

        //Increment through x values using maxY
        var x = 0;
        while (x >= maxX) {
            tilemapGround.SetTile(new Vector3Int(x, maxY, 0), groundTile);
            x -= 1;
        }

        tilesDict = tileManager.getTilesDictionary();
    }

}
