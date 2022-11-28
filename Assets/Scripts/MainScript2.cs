using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript2 : MonoBehaviour
{
    public GameObject pawnRed, pawnBlue, redArtillery, blueArtillery, redBoat, blueBoat, redAir, blueAir;

    public Camera pickCamera;

    public GameObject map;

    GameObject selection = null;

    //Private Variables
    private MapMake2 mapData;

    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Get the script from the object, then make a data
        mapData = map.GetComponent<MapMake2>();
        mapData.MapMake(level);

        //spawnBlueOnGrass();
        _ = Instantiate(pawnBlue, new Vector3(0, 0.5f, 0), pawnBlue.transform.rotation) as GameObject;
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
                    selection.transform.position = hitInfo.transform.position;
                    selection.SendMessage("Highlight", false);
                    selection = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selection)
        {
            selection.SendMessage("Highlight", false);
            selection = null;
        }
       
        // redChaseBlue();
    }

    void spawnBlueOnGrass()
    {
        GameObject firstGrass = GameObject.FindWithTag("Grass Tile");
        float XPos = firstGrass.transform.position.x;
        float YPos = firstGrass.transform.position.z;
        Debug.Log(XPos + " " + YPos);
        _ = Instantiate(pawnBlue, new Vector3(XPos, 0.5f, YPos), pawnBlue.transform.rotation) as GameObject;
    }

    void redChaseBlue()
    {
        // Red Pawn chase after the Blue Pawn
        GameObject bluepawn = GameObject.FindWithTag("Blue Pawn");
        GameObject redPawn = GameObject.FindWithTag("Red Pawn");
        Vector3 posB = bluepawn.transform.position;
        redPawn.SendMessage("Move", posB);
    }

    void printTileTypeMap()
    {

    }
}
