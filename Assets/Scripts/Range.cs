using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    public LineRenderer circle;
    public int range;
    public Vector3 currentlocation;


    // Start is called before the first frame update
    void Start()
    {
        DrawRange(100, (float)range*1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawRange(int steps, float radius)
    {
        circle.positionCount = steps;

        for(int currentStep = 0; currentStep<steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadien = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadien);
            float yScaled = Mathf.Sin(currentRadien);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x + currentlocation.x, 0.1f, y + currentlocation.z);

            circle.SetPosition(currentStep, currentPosition);

        }
    }

    

}
