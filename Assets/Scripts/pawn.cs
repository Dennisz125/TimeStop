using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn: MonoBehaviour
{
    Vector3[] cardinals;
    float[] directions = { 0.0f, 60.0f, 120.0f, 180.0f, 240.0f, 300.0f };
    int dir;

    static int i = 0;
    Vector3 target, deltaV;
    bool inMotion;
    float speed;

    int tileX, tileY;

    Material original = null;
    public Material Outline;

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
        dir = 0;

        inMotion = false;        
    }

    void setTarget(Vector3 destination)
    {
        target = destination;
        deltaV = target - transform.position;
        deltaV = Vector3.Normalize(deltaV);
        speed = 1.0f;
        inMotion = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (!inMotion)
        {
            setTarget(transform.position + new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f)));
        }*/

        Vector3 remaining = target - transform.position;

        if (remaining.magnitude < 1.0f)
        {
            speed = 0.5f;
        }

        if (remaining.magnitude < .5f)
        {
            speed = 0.25f;
        }

        if (remaining.magnitude < .25f)
        {
            speed = 0.125f;
        }

        if (remaining.magnitude < Time.deltaTime * speed)
        {
            inMotion = false;
            transform.Translate(remaining);
        }
        else
        {
            // assumes that deltaV is normalized
            transform.Translate(deltaV * Time.deltaTime * speed, Space.World);
        }
    }

    void Highlight(bool state)
    {
        if (state)
        {

            original = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = Outline;
        }
        else
        {
            GetComponent<Renderer>().material = original;
        }
    }

    void Move(Vector3 dest)
    {
        if (dest != null) {
            
            // send in a direction
            dest.y = 0.5f;
            setTarget(dest);
            
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name/tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Blue Pawn")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Blue Pawn Gone");
            Destroy(collision.gameObject);
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        else if (collision.gameObject.tag == "Red Pawn")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Red Pawn Gone");
            Destroy(collision.gameObject);
        }
    }
}
