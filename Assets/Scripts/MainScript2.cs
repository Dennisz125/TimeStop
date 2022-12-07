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

    private int actionpoints = 10;

    [SerializeField] private ChangeTurn changeTurnScript;
    [SerializeField] private GameObject playerWonCanvas;

    [SerializeField] private GameObject explosionparticles;
    
 
    private GameObject rangeindicator;

    // UI related fields
    [SerializeField] private GameObject pawnInfoUI;
    [SerializeField] private GameObject pawnUIHealth;
    [SerializeField] private GameObject pawnUIMovementSpeed;
    [SerializeField] private GameObject pawnUIAttackRange;
    [SerializeField] private TextMeshProUGUI turnInfo;
    [SerializeField] private TextMeshProUGUI roundInfo;
    [SerializeField] private TextMeshProUGUI actionInfo;
    private static bool gameIsPaused = false;
    private float defaultPauseTime = 2.5f;
    private float pauseTime = 2.5f;


    public int level = 1;
    private int roundNum;
    private bool endTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        //Get the script from the object, then make a data
        mapData = map.GetComponent<MapMake2>();
        mapData.MapMake(level);

        // spawning blue army
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 0), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 1), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 2), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 3), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 4), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 5), blueTeam);
        spawnNewPieceAt(pawnBlue, new Vector2Int(2, 6), blueTeam);
        spawnNewPieceAt(blueArtillery, new Vector2Int(1, 2), blueTeam);
        spawnNewPieceAt(blueArtillery, new Vector2Int(1, 3), blueTeam);
        spawnNewPieceAt(blueArtillery, new Vector2Int(1, 4), blueTeam);

        // spawning red army
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 0), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 1), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 2), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 3), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 4), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 5), redTeam);
        spawnNewPieceAt(pawnRed, new Vector2Int(4, 6), redTeam);
        spawnNewPieceAt(redArtillery, new Vector2Int(5, 2), redTeam);
        spawnNewPieceAt(redArtillery, new Vector2Int(5, 3), redTeam);
        spawnNewPieceAt(redArtillery, new Vector2Int(5, 4), redTeam);
        pawnInfoUI.SetActive(false);
        roundNum = 1;
        roundInfo.text = "Round " + roundNum;
        
        playerWonCanvas.SetActive(false);

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
        if (gameOver) { return; }

        if (gameIsPaused)
        {
            decrementPauseTimer();
            return;
        }
        switch (gameStates)
        {
            case GameStates.player1Turn:
                playerLoop(0);
                turnInfo.text = "Player 1's Turn";
                actionInfo.text = actionpoints.ToString();
                roundInfo.text = "Round " + roundNum;
                if (endTurn)
                {
                    changeTurnScript.FadeToNextTurn();
                    
                    gameStates = GameStates.player2Turn;
                    endTurn = false;
                    actionpoints = 10;
                    gameIsPaused = true;
                }
                break;
            case GameStates.player2Turn:
                playerLoop(1);
                turnInfo.text = "Player 2's Turn";
                actionInfo.text = actionpoints.ToString();
                if (endTurn)
                {
                    changeTurnScript.FadeToNextTurn();
                    
                    gameStates = GameStates.executionTurn;
                    endTurn = false;
                    actionpoints = 10;
                    gameIsPaused = true;
                }
                break;
            case GameStates.executionTurn:
                // execution logic ...
                turnInfo.text = "Executing";

                execution();
                recalculateTeamPawns();

                // check if a team has won the game
                int redTeamCount = redTeam.transform.childCount;
                int blueTeamCount = blueTeam.transform.childCount;
                print("Red Team Count:" + redTeamCount);
                print("Blue Team Count:" + blueTeamCount);
                checkIfTeamWon(blueTeamCount, redTeamCount);

                break;
        }
    }

    private void decrementPauseTimer()
    {
        pauseTime -= Time.deltaTime;
        if (pauseTime <= 0)
        {
            gameIsPaused = false;
            pauseTime = defaultPauseTime;
        }
    }

    void checkIfTeamWon(int blueTeamCount, int redTeamCount)
    {
        if (blueTeamCount == 0 && redTeamCount != 0)
        {
            print("Player 2 wins");
            turnInfo.text = "Player 2 Wins";
            gameOver = true;
            playerWonCanvas.SetActive(true);
            string wonText = "Player 2 won!";
            playerWonCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText(wonText);
        }
        else if (blueTeamCount != 0 && redTeamCount == 0)
        {
            print("Player 1 wins");
            turnInfo.text = "Player 1 Wins";
            gameOver = true;
            playerWonCanvas.SetActive(true);
            string wonText = "Player 1 won!";
            playerWonCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText(wonText);
        }
        if (blueTeamCount == 0 && redTeamCount == 0)
        {
            print("Tie");
            turnInfo.text = "Tie";
            gameOver = true;
            playerWonCanvas.SetActive(true);
            string wonText = "Tie!";
            playerWonCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText(wonText);
        }
        else
        {
            //changeTurnScript.FadeToNextTurn();
            gameStates = GameStates.player1Turn;
            roundNum++;
        }
    }

    void recalculateTeamPawns()
    {
        foreach (Transform child in redTeam.transform)
        {
            if (child.GetComponent<pawn>().healthPoints <= 0)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in blueTeam.transform)
        {
            if (child.GetComponent<pawn>().healthPoints <= 0)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    void playerLoop(int state)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(pickCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, ~2);

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



                    //check if ghost
                   

                if (selectionScript.checkNeighbors(hitInfoPosition))
                    {
                        if (actionpoints > 0)
                        {
                              if (!selectionScript.isGhost)
                              {
                                if (selectionScript.getTeamOwner() == 1)
                                {
                                 selectionScript.setfrom(spawnNewPieceAt(pawnBlue, selectionScript.getPosition(), blueTeam));
                                
                                }
                                else if (selectionScript.getTeamOwner() == 2)
                                {
                                 selectionScript.setfrom(spawnNewPieceAt(pawnRed, selectionScript.getPosition(), redTeam));
                                }
                               }

                        
                            selection.transform.position = hitInfo.transform.position;
                            selectionScript.setPosition(hitInfoPosition);
                            selection.SendMessage("Highlight", false);
                            
                            
                            //ghost pawn
                            if (!selectionScript.isGhost) selectionScript.isGhost = true;
                            selectionScript.beGhost();
                            selection = null;
                            actionpoints--;
                            actionInfo.text = actionpoints.ToString();
                        }
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

    void execution()
    {
        MovePawn();
        //For every blue pawn in the team
        foreach (Transform blue in this.blueTeam)
        {
            pawn blueScript = blue.GetComponent<pawn>();
            blueScript.makeNeighbors();

            //For every red pawn in the team
            foreach (Transform red in this.redTeam)
            {
                pawn redScript = red.GetComponent<pawn>();
                redScript.makeNeighbors();
                //if red and blue are next to each other, then they both attack
                Vector2Int[] temp = blueScript.getNeighbors();
                Vector2Int redPos = redScript.getPosition();
                foreach (Vector2Int bluePos in temp)
                {
                    if (bluePos == redPos)
                    {
                        --blueScript.healthPoints;
                        --redScript.healthPoints;
                        PlayFireParticle(blue.position);
                        continue;
                    }
                }
                
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


    GameObject spawnNewPieceAt(GameObject newObject, Vector2Int newPosition, Transform parent)
    {
        if (newObject == null || newPosition == null) return null;
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

        return newSpawn;
    }


    public void endTurnFunction()
    {
        endTurn = true;
    }

    public void MovePawn ()
    {
        
            GameObject[] bluegos = GameObject.FindGameObjectsWithTag("Blue Pawn");
            foreach (GameObject go in bluegos)
            {
                pawn pawnscript;
                go.TryGetComponent<pawn>(out pawnscript);
                if (pawnscript.isGhost)
                {
                    pawnscript.Move();
                }
            }
        
        
            GameObject[] redgos = GameObject.FindGameObjectsWithTag("Red Pawn");
            foreach (GameObject go in redgos)
            {
                pawn pawnscript;
                go.TryGetComponent<pawn>(out pawnscript);
                if (pawnscript.isGhost)
                {
                    pawnscript.Move();
                }
            }
        
    }


    GameObject PlayFireParticle(Vector3 position)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        emitParams.velocity = new Vector3(0.0f, 0.0f, -2.0f);

        GameObject particles = Instantiate(explosionparticles, position, explosionparticles.transform.rotation);

        explosionparticles.GetComponent<ParticleSystem>().Emit(emitParams, 1);

        return particles;
    }


    void printTileTypeMap()
    {

    }

    public void restartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
