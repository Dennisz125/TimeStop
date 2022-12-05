using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

enum GamePhase
{
    Player1 = 1,
    Player2 = 2,
    Execution = 3
}

public class MainScript2 : MonoBehaviour
{
    public Transform blueTeam, redTeam;
    public GameObject pawnRed, pawnBlue, redArtillery, blueArtillery, redBoat, blueBoat, redAir, blueAir;

    public Camera pickCamera;

    public GameObject map;

    public GameObject selection = null;

    public GameObject rangeindicatorprefab;

    //Private Variables
    private MapMake2 mapData;
    private VictoryConditions victoryConditions = new VictoryConditions();
    private MainUI UI = new MainUI();
    private float xOffset = 1.73f;
    private float zOffset = 1.5f;
    private GamePhase currentPhase = GamePhase.Player1;

    private GameObject rangeindicator;

    // UI related fields
    [SerializeField] private GameObject pawnInfoUI;
    [SerializeField] private GameObject pawnUIHealth;
    [SerializeField] private GameObject pawnUIMovementSpeed;
    [SerializeField] private GameObject pawnUIAttackRange;

    
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
        pawnInfoUI.SetActive(false);

        //spawnBlueOnGrass();
        //spawnBlueOnGrass();
        ////Get the script from the object
        
        //Breakpoint
    }

    private enum GameStates
    {
        player1Turn = 0,
        player2Turn = 1,
        executionTurn = 2
    }
    private GameStates gameStates = GameStates.player1Turn;



    private bool gameOver = false;
    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        switch (gameStates)
        {
            case GameStates.player1Turn:
                playerLoop(0);
                    
                // if player 1 selects "end turn" button, switch to player 2 turn
                // gameStates = GameStates.player2Turn;
                break;
            case GameStates.player2Turn:
                playerLoop(1);
                    
                // if player 2 selects "end turn" button, switch to execution turn
                //gameStates = GameStates.executionTurn;
                break;
            case GameStates.executionTurn:
                // execution logic ...
                    
                // check if a team has won the game
                (bool, int) winnerTeam = victoryConditions.checkIfTeamWon();
                if (winnerTeam.Item1)
                {
                    print("winner! team:" + winnerTeam.Item2);
                    gameOver = true;
                }
                    
                // if the game is done executing, switch to player 1 turn
                //gameStates = GameStates.player1Turn;
                break;
        }
    }

    void playerLoop(int state)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(pickCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit && hitInfo.transform.gameObject.name != "flat" && hitInfo.transform.gameObject.tag != "HexTile")
            {
                bool selectedPawnFromTeam1 = hitInfo.transform.gameObject &&
                                             hitInfo.transform.gameObject.GetComponent<pawn>() && hitInfo.transform
                                                 .gameObject.GetComponent<pawn>().getTeamOwner() == 1;
                bool selectedPawnFromTeam2 = hitInfo.transform.gameObject &&
                                             hitInfo.transform.gameObject.GetComponent<pawn>() && hitInfo.transform
                                                 .gameObject.GetComponent<pawn>().getTeamOwner() == 2;
                //selection.name = hitInfo.transform.gameObject.name;
                if ((state == 0 && selectedPawnFromTeam1) || (state == 1 && selectedPawnFromTeam2))
                {
                    selectPawnHelper(hitInfo);
                }
            }
            else
            {
                // disable pawn info UI
                pawnInfoUI.SetActive(false);
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
                        if (rangeindicator)
                        {
                            Destroy(rangeindicator);
                        }
                    }
                    
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selection)
        {
            selection.SendMessage("Highlight", false);
            selection = null;
            if (rangeindicator)
            {
                Destroy(rangeindicator);
            }
        }

        
    }

    private void selectPawnHelper(RaycastHit hitInfo)
    {
        Debug.Log(selection + " has been picked");
        if (!selection || selection != hitInfo.transform.gameObject)
        {
            if (selection)
            {
                selection.SendMessage("Highlight", false);

                if (rangeindicator)
                {
                    Destroy(rangeindicator);
                }
            }
            selection = hitInfo.transform.gameObject;

            selection.SendMessage("Highlight", true);

            //show range
            Range rangeind = rangeindicatorprefab.GetComponent<Range>();
            pawn pawnscript = selection.GetComponent<pawn>();
            rangeind.range = pawnscript.attackRange;
            rangeind.currentlocation = selection.transform.position;
            rangeindicator = Instantiate(rangeindicatorprefab);


            //update UI 
            pawnInfoUI.SetActive(true);
            pawnUIHealth.GetComponent<TextMeshProUGUI>().text = hitInfo.transform.gameObject.GetComponent<pawn>().healthPoints.ToString();
            pawnUIAttackRange.GetComponent<TextMeshProUGUI>().text = hitInfo.transform.gameObject.GetComponent<pawn>().attackRange.ToString();
            pawnUIMovementSpeed.GetComponent<TextMeshProUGUI>().text = hitInfo.transform.gameObject.GetComponent<pawn>().movementSpeed.ToString();
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
