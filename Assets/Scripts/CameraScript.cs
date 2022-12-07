using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CameraScript : MonoBehaviour
{
    private Vector3 Origin;

    private bool drag = false;
    private float mapMinX, mapMaxX, mapMinZ, mapMaxZ;

    [SerializeField] private Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        mapMinX = 4;
        mapMaxX = 38;
        mapMinZ = 0;
        mapMaxZ = 20;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
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

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Origin = Camera.ScreenToViewportPoint(Input.mousePosition);

        }
        if(Input.GetMouseButton(0))
        {
            Vector3 Difference = new Vector3(Origin.x - Camera.ScreenToViewportPoint(Input.mousePosition).x, 0, Origin.y - Camera.ScreenToViewportPoint(Input.mousePosition).y);
            Camera.transform.position = ClampCamera(Camera.transform.position + Difference);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = Camera.orthographicSize;
        float camWidth = Camera.orthographicSize * Camera.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minZ = mapMinZ + camHeight;
        float maxZ = mapMaxZ - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        return new Vector3(newX, targetPosition.y, newZ);
    }
    public void focusCamera(Vector3 position)
    {
        transform.position = new Vector3(position.x, 15,position.z);
        //transform.Translate(position.x, position.y, -Input.GetAxis("Zoom"));
    }
}