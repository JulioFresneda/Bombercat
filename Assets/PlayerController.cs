using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {


    public Tilemap tilemap;
    public float velocityPlayer = 4f;
    public int lifes = 3;
    public int explosionRange = 1;
    public int bombsTogether = 1;

    public Tile wallTile;
    public Tile destructibleTile;

    private new Rigidbody2D rigidbody;

    public GameObject goal;
    public bool win = false;

    private Vector3 respawn;

    public GameObject Enemy;

    public int score;
    public int level;

    // Use this for initialization
    void Start () {

        score = 0;
        level = 1;

        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        Vector3 playerPos = gameObject.GetComponent<Transform>().position;
        respawn = playerPos;
        Vector3Int cell = tilemap.WorldToCell(playerPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
        gameObject.transform.position = cellCenterPos;
        
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        InGoal();
	}

    void Move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector2(-velocityPlayer, 0);
        }
        else if( Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector2(velocityPlayer, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = new Vector2(0,velocityPlayer);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity = new Vector2(0,-velocityPlayer);
        }
        else rigidbody.velocity = Vector2.zero;
    }


    bool IsValidCellToMove(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile || tile == destructibleTile)
        {
            return false;
        }
        else return true;
    }

    public void Killed()
    {
        if (lifes > 1)
        {
            lifes--;
            gameObject.GetComponent<Transform>().position = respawn;
        }
        // else Destroy(gameObject);
    }

    void InGoal()
    {
        if( tilemap.WorldToCell(goal.GetComponent<Transform>().position) == tilemap.WorldToCell(gameObject.GetComponent<Transform>().position))
        {
            win = true;
            // WIN
        }
    }
}