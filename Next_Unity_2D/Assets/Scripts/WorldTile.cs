using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile {
    
    //Where our tile is sitting on our grid
    public Vector3Int TileGridPos { get; set; }

    //What kind of tile is it?
    public TileBase TileBase { get; set; }

    //Which tilemap this tile is member of
    public Tilemap TilemapMember { get; set; }

    //Which state is the tile in?
    public int TileState { get; set; }

}
