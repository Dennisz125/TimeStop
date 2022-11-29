using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject pawnRed, pawnBlue, redArtillery, blueArtillery;

    public Camera pickCamera;

    public GameObject map;

    GameObject selection = null;

    // Start is called before the first frame update
    void Start()
    {
        map.SendMessage("mapMake1");

        GameObject unit;
        for (int i = 0; i < 5; i++)
        {
            unit = Instantiate(pawnRed, new Vector3(-2.0f+(float)i * 2, 0.7f, -3.0f), pawnRed.transform.rotation) as GameObject;
            unit.gameObject.tag = "Red Pawn";
        }
        for (int i = 0; i < 5; i++)
        {
            unit = Instantiate(pawnBlue, new Vector3(-2.0f+(float)i * 2, 0.7f, 3.0f), pawnBlue.transform.rotation) as GameObject;
            unit.gameObject.tag = "Blue Pawn";
        }
        for (int i = 0; i < 3; i++)
        {
            unit = Instantiate(redArtillery, new Vector3(-2.0f + (float)i * 2, 0.7f, -6.0f), redArtillery.transform.rotation) as GameObject;
            unit.gameObject.tag = "Red Artillery";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(pickCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit && hitInfo.transform.gameObject.name != "flat")
            {
                Debug.Log(selection + " has been picked");
                if (!selection || selection != hitInfo.transform.gameObject) {
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

                if (selection) {
                    Vector3 dst = hitInfo.point;
                    selection.SendMessage("Move", dst);
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

        
        // Red Pawn chase after the Blue Pawn
        GameObject bluepawn = GameObject.FindWithTag("Blue Pawn");
        GameObject redPawn = GameObject.FindWithTag("Red Pawn");
        if (bluepawn != null && redPawn != null)
        {
            Vector3 posB = bluepawn.transform.position;
            redPawn.SendMessage("Move", posB);
        }
        
        
    }
}
