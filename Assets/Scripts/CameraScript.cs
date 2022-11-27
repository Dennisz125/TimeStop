using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        float xval = Input.GetAxis("Horizontal");
        float yval = Input.GetAxis("Zoom");
        float fwdIn = Input.GetAxis("Vertical");
        float rval = Input.GetAxis("Rotate");
        float r2val = Input.GetAxis("Rotate2");
        float r3val = Input.GetAxis("Rotate3");

        // Set Camera Position (A and D and Zoom)
        transform.Translate(new Vector3(0.0f, 0.0f, -yval)* Time.deltaTime, Space.Self);
        // reset Height
        float yComp = transform.up.y;
        transform.Translate(new Vector3(xval, Mathf.Sin(Mathf.Acos(yComp)) * fwdIn, yComp * fwdIn) * Time.deltaTime, Space.Self);
        // Rotate at center of camera
        transform.RotateAround(transform.position, Vector3.up, -rval * 20.0f * Time.deltaTime);
        transform.Rotate(-r3val * 20.0f * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        if(Input.GetMouseButtonDown(1))
        {
            transform.RotateAround(transform.position, Vector3.up, -r2val);
        }
    }
}