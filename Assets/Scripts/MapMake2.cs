using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class MapMake2 : MonoBehaviour
{

    public GameObject grass_hex, water_hex, desert_hex;
    GameObject spawnHex;

    public GameObject selectedUnit;

    public TileType[] tileTypes;

    int[,] tiles;


    // Size of the map in terms of hex tiles
    public int width, height;

    float xOffset = 1.73f;
    float zOffset = 1.5f;

    void MapMake(int level)
    {
        /* Makes a map for scene *Map2*
         * Make and save seed
         * Make a 2d list of cords of hexs
         */
        Random.seed = level;

        // Allocate our map tiles
        tiles = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;

                // On the odd row
                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }
                int randomHexInt = Random.Range(0, 3);

                switch (randomHexInt)
                {
                    case 0:
                        spawnHex = grass_hex;
                        break;
                    case 1:
                        spawnHex = water_hex;
                        break;
                    case 2:
                        spawnHex = desert_hex;
                        break;
                }

                GameObject hex_tile = Instantiate(spawnHex, new Vector3(xPos, 0, y * zOffset), spawnHex.transform.rotation) as GameObject;
                var meshCollider = hex_tile.AddComponent<MeshCollider>();

                hex_tile.gameObject.tag = "HexTile";
                hex_tile.name = "Hex_" + x + "_" + y;
                //hex_tile.setTileXY(x, y);
                hex_tile.transform.SetParent(this.transform);
                //allTiles.Add(x*1000+y, hex_tile);

                
                /*
                if (spawnHex == grass_hex)
                {
                    hex_tile.gameObject.tag = "Grass Tile";
                }
                */

                

                //Generate data for the map 
                tiles[x, y] = randomHexInt;

                // TODO: FIX THE BUG RELATED TO THE LINE BELOW
                //tileTypeList[x, y] = tileType;
                //Debug.Log(hex_tile.name + " has spawned");
            }
        }
    }
    void returnTiles(ref int[,] tiles)
    {
        /*Returns and gives tiles to MainScript
        */
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x,y] = this.tiles[x, y];
            } 
        }
    }
}
