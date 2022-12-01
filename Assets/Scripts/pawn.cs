using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class pawn: MonoBehaviour
{
    Vector3[] cardinals;
    float[] directions = { 0.0f, 60.0f, 120.0f, 180.0f, 240.0f, 300.0f };
    int dir;

    static int id = 0;
    Vector3 target, deltaV;
    bool inMotion;
    float speed;
    [SerializeField] Vector2Int[] neighbors;

    [SerializeField] int tileX, tileY;
    [SerializeField] public int healthPoints = 1;
    [SerializeField] public int actionPoints;
    [SerializeField] public int attackRange = 1;
    [SerializeField] public int movementSpeed = 1;

    Material original = null;
    public Material Outline;

    [SerializeField] private int teamOwner = 0;

    [SerializeField] private bool isHighlighted = false;

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
        neighbors = new Vector2Int[6];
    }
    
    public void setTeamOwner(int teamOwner)
    {
        this.teamOwner = teamOwner;
    }

    public int getTeamOwner()
    {
        return this.teamOwner;
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

    public void setPosition(Vector2Int newPosition)
    {
        this.tileX= newPosition.x;
        this.tileY= newPosition.y;
    }

    public Vector2Int getPosition()
    {
        return new Vector2Int(this.tileX, this.tileY);
    }

    public bool checkNeighbors(Vector2 newHexPosition)
    {

        //check 2 tile on the left
        if (newHexPosition == null) return false;
        //check 2 tile on the right
        //check top and botton
        return true;
    }

    public void makeNeighbors()
    {
        //TODO
        //This is brute force, if you have time, refine this block
        neighbors = new Vector2Int[6];
        if (tileY % 2 != 0)
        {
            neighbors[0] = new Vector2Int(tileX, tileY + 1);
            neighbors[1] = new Vector2Int(tileX - 1, tileY);
            neighbors[2] = new Vector2Int(tileX, tileY - 1);
            neighbors[3] = new Vector2Int(tileX + 1, tileY + 1);
            neighbors[4] = new Vector2Int(tileX + 1, tileY);
            neighbors[5] = new Vector2Int(tileX + 1, tileY - 1);
        } else
        {
            neighbors[0] = new Vector2Int(tileX - 1, tileY - 1);
            neighbors[1] = new Vector2Int(tileX - 1, tileY);
            neighbors[2] = new Vector2Int(tileX - 1, tileY + 1);
            neighbors[3] = new Vector2Int(tileX, tileY + 1);
            neighbors[4] = new Vector2Int(tileX + 1, tileY);
            neighbors[5] = new Vector2Int(tileX, tileY - 1);
        }
        
    }
    public bool checkNeighbors(Vector2Int position)
    {
        foreach (Vector2Int neighbor in neighbors)
        {
            //Edge case
            if (neighbor.x < 0 || neighbor.y < 0 || neighbor == null) continue;
            if (neighbor == position) return true;
        }
        return false;
    }
    public bool checkNeighbors(Vector3 position)
    {
        foreach (Vector2Int neighbor in neighbors)
        {
            //Edge case
            if (neighbor.x < 0 || neighbor.y < 0 || neighbor == null) continue;
            if (neighbor.x == position.x && neighbor.y == position.y) return true;
        }
        return false;
    }
}
