using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMangement : MonoBehaviour
{

    GameObject selection = null;
    public Camera pickCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo) && Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            MeshRenderer mesh = hitObject.GetComponent<MeshRenderer>();

            // orginal color of the tiles
            Color orginal = mesh.material.color;

            if(mesh.material.color == Color.red)
            {
                mesh.material.color = Color.blue;
            }
            else
            {
                mesh.material.color = Color.red;
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            bool hit = Physics.Raycast(pickCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit && hitInfo.transform.gameObject.name != "flat")
            {
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

                if (selection)
                {
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
    }
}
