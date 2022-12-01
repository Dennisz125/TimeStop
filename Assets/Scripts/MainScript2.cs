using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript2 : MonoBehaviour
{
    public Transform blueTeam, redTeam;
    public GameObject pawnRed, pawnBlue, redArtillery, blueArtillery, redBoat, blueBoat, redAir, blueAir;

    public Camera pickCamera;

    public GameObject map;

    GameObject selection = null;

    //Private Variables
    private MapMake2 mapData;
    private VictoryConditions victoryConditions = new VictoryConditions();
    private MainUI UI = new MainUI();
    private float xOffset = 1.73f;
    private float zOffset = 1.5f;

    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Get the script from the object, then make a data
        mapData = map.GetComponent<MapMake2>();
        mapData.MapMake(level);

        spawnNewPieceAt(pawnBlue, new Vector2Int(0, 1), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(0, 0), blueTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(3, 0), redTeam);

        //spawnBlueOnGrass();
        //spawnBlueOnGrass();
        ////Get the script from the object
        
        //Breakpoint
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(pickCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit && hitInfo.transform.gameObject.name != "flat" && hitInfo.transform.gameObject.tag != "HexTile")
            {
                //selection.name = hitInfo.transform.gameObject.name;

                Debug.Log(selection + " has been picked");
                if (!selection || selection != hitInfo.transform.gameObject)
                {
                    if (selection)
                    {
                        selection.SendMessage("Highlight", false);
                    }
                    selection = hitInfo.transform.gameObject;
                    selection.SendMessage("Highlight", true);

                    //update UI 
                    

                }
            }
            else
            {

                if (selection && hitInfo.transform.gameObject.tag == "HexTile")
                {
                    /*
                    Vector3 dst = hitInfo.point;
                    selection.SendMessage("Move", dst);
                    */
                    //Get scripts from selection and hitInfo
                    pawn selectionScript = selection.GetComponent<pawn>();
                    ClickableTile hitInfoScript = hitInfo.transform.GetComponent<ClickableTile>();

                    Vector2Int hitInfoPosition = hitInfoScript.getTileXYPosition();
                    selectionScript.makeNeighbors();
                    if(selectionScript.checkNeighbors(hitInfoPosition))
                    {
                        selection.transform.position = hitInfo.transform.position;
                        selectionScript.setPosition(hitInfoPosition);
                        selection.SendMessage("Highlight", false);
                        selection = null;
                    }
                    
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selection)
        {
            selection.SendMessage("Highlight", false);
            selection = null;
        }
       
        // redChaseBlue();
        
        
        // check if a team has won the game
        (bool, int) winnerTeam = victoryConditions.checkIfTeamWon();
        if (winnerTeam.Item1)
        {
            print("winner! team:" + winnerTeam.Item2);
        }
    }


    void spawnNewPieceAt(GameObject newObject, Vector2Int newPosition, Transform parent)
    {
        if (newObject == null || newPosition == null) return;
        float xPos = newPosition.x * xOffset;
        // On the odd row
        if (newPosition.y % 2 == 1)
        {
            xPos += xOffset / 2f;
        }
        
        GameObject newSpawn = Instantiate(newObject, new Vector3(xPos, 0.5f, newPosition.y * zOffset), newObject.transform.rotation, parent) as GameObject;
        pawn newPawnScript = newSpawn.GetComponent<pawn>();
        newPawnScript.setPosition(newPosition);
        if (parent == this.blueTeam)
        {
            newPawnScript.setTeamOwner(1);
        } else if (parent == this.redTeam)
        {
            newPawnScript.setTeamOwner(2);
        }
        newPawnScript.makeNeighbors();
        
    }

    void printTileTypeMap()
    {

    }
}
