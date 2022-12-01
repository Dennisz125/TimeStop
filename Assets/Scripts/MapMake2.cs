using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class MapMake2 : MonoBehaviour
{
    // Models it use to spawn
    public GameObject grass_hex, water_hex, desert_hex;
    
    // 2D Map, each cell is a TileType for data storage
    public TileType[,] tileTypesMap;

    // Size of the map in terms of hex tiles
    public int width, height;

    // Private and local variable
    private GameObject spawnHex;
    private Type spawnType;
    private bool isWater = false;
    private float xOffset = 1.73f;
    private float zOffset = 1.5f;

    public void MapMake(int level)
    {
        /* Makes a map for scene *Map2*
         * Make and save seed
         * Make a 2d list of cords of hexs
         */
        Random.InitState(level);

        // Allocate our map tiles
        tileTypesMap = new TileType[width, height];

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

                int randomMin = 0;
                int randomMax = 6;
                int tile1Match = 0;
                int tile2Match = 0;
                int tile3Match = 0;
                if (x > 0 && y > 0 && x < width - 1 && y < height - 1)
                {
                    // get tempTile1 = x-1,y-1 tile type
                    // get tempTile2 = x-1,y tile type
                    // get tempTile3 = x,y-1 tile type
                    
                    // if tempTile1 == tile type 1
                    // tile1Match++
                    // else if tempTile1 == tile type 2
                    // tile2Match++
                    // else if tempTile1 == tile type 3
                    // tile3Match++
                    
                    // repeat for remaining 2 tiles
                }
                int randomHexInt = Random.Range(randomMin, randomMax);

                // if (randomHexInt >= 0 && randomHexInt <= )
                
                // Get Temp Model and Type
                /*switch (randomHexInt)
                {
                    case 0:
                        spawnHex = grass_hex;
                        spawnType = Type.Grass;
                        break;
                        
                    case 1:
                        spawnHex = water_hex;
                        spawnType = Type.Water;
                        isWater= true;
                        break;
                    case 2:
                        spawnHex = desert_hex;
                        spawnType = Type.Desert;
                        break;
                        
                }*/

                // Spawns and render new Hex into the Game
                GameObject hex_tile = Instantiate(spawnHex, new Vector3(xPos, 0, y * zOffset), spawnHex.transform.rotation) as GameObject;
                var meshCollider = hex_tile.AddComponent<MeshCollider>();

                // Set Name and Tag, Set Current Object using this script as parent
                hex_tile.gameObject.tag = "HexTile";
                hex_tile.name = "Hex_" + x + "_" + y;
                hex_tile.transform.SetParent(this.transform);

                //hex_tile.setTileXY(x, y);
                //allTiles.Add(x*1000+y, hex_tile);

                /*
                if (spawnHex == grass_hex)
                {
                    hex_tile.gameObject.tag = "Grass Tile";
                }
                */

                //Generate data for the map | TileType(string name, Type type, int XPos, int YPos, bool isWater)
                tileTypesMap[x, y] = new TileType(hex_tile.name, spawnType, x, y, isWater);
                isWater = false;
            }
        }
    }
    public TileType[,] getTileTypeMap()
    {
        return this.tileTypesMap;
    }

    public Vector2Int getTileTypePos()
    {
        return new Vector2Int(0, 0);
    }

    public void deleteTilesTypeMapData()
    {
        //Set every cell to NULL then deletes the table
        //Gives tiles to Other Script
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.tileTypesMap[x, y] = null;
            }
        }
        this.tileTypesMap = null;
    }
}
