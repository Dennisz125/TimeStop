using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileType
{
    public string name;
    public GameObject type;
    public int XPostion, YPostion;

    public bool isWalkable = true;
    public bool isWater = false;

    public GameObject tileVisualPrefab;
    public float movementCost = 1;

    public static GameObject DEFAULTTYPE;
    public TileType() : this("Unnamed", DEFAULTTYPE, 0, 0, false)
    {
    }
    public TileType(string name, GameObject type, int XPos, int YPos, bool isWater)
    {
        this.name = name;
        this.type = type;
        XPostion = XPos;
        YPostion = YPos;
        this.isWater = isWater;

    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        cardinals = new Vector3[]
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.5f, 0.0f, .866025f),
            new Vector3(-0.5f, 0.0f, .866025f),
            new Vector3(-1.0f, 0.0f, 0.0f),
            new Vector3(-0.5f, 0.0f, -.866025f),
            new Vector3(0.5f, 0.0f, -.866025f),
        };
        /*
        GameObject tile;
        GameObject typeObj;

        switch (typeObj)
        {
            case spawnWater:
                // buff and debuff
                break;
            case spawnSand:
                // buff and debuff
                break;
            case spawnHill:
                // buff and debuff
                break;
            case spawnBuilding:
                // buff and debuff
                break;
            case spawnTree2:
                // buff and debuff
                break;
            case spawnTree1:
                // buff and debuff
                break;
            case spawnBuilding2:
                // buff and debuff
                break;
            case spawnGrass:
                // buff and debuff
                break;
            default:
                typeObj = null;
                break;
        }

        if (typeObj)
        {
            // attach the buff and debuff to the hex tile
            tile = null;
        }
         
        // Update is called once per frame
        void Update()
        {

        }

        void Picked(GameObject selection)
        {
            
            switch (typeObj)
            {
                case spawnWater:
                    Console.WriteLine("This is Water");
                    break;
                case spawnSand:
                    Console.WriteLine("This is Sand");
                    break;
                case spawnHill:
                    Console.WriteLine("This is Hill");
                    break;
                case spawnBuilding:
                    Console.WriteLine("This is Building");
                    break;
                case spawnTree2:
                    Console.WriteLine("This is Trees 2");
                    break;
                case spawnTree1:
                    Console.WriteLine("This is Trees 1");
                    break;
                case spawnBuilding2:
                    Console.WriteLine("This is Building 2");
                    break;
                case spawnGrass:
                    Console.WriteLine("This is Grass");
                    break;
                default:
                    typeObj = null;
                    break;
                    
            }
        }
        */

}

