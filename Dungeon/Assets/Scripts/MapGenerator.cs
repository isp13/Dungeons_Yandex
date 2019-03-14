using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    
    public Tilemap tilemap;
    public Tile floor;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            func();
    }

    void func(int[,] map)
    {
        
        BoundsInt bounds = tilemap.cellBounds; // getting all allowed x and y in tilemap (getting its size)
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds); // getting info of tiles from tilemap
        Vector3Int currentCell = tilemap.WorldToCell(transform.position); //current x,y,z position 

        for (int x = 0; x < map.GetLength(0); ++x)
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floor);
            }
        }
    }
}
