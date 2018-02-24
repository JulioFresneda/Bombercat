using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{


    public Tilemap tilemap;
    public float velocityEnemy = 0.1f;

    public Tile wallTile;
    public Tile destructibleTile;

    public GameObject player;

    private new Rigidbody2D rigidbody;

    private int axisX;
    private int axisY;

    Vector3Int enemyCell;
    Vector2 enemyPos;
    Vector3Int enemyNextCell;

    bool changeDirection;

    public static int numEnemies = 0;

     

    // Use this for initialization
    void Start()
    {

        numEnemies++;
        if (numEnemies < 5) Instantiate(gameObject);
        RandomDirection();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        //rigidbody.position = GameObject.FindGameObjectWithTag("EnemyGenerator").GetComponent<Transform>().position;


        CentrarPos();

        rigidbody.velocity = new Vector2(axisX * velocityEnemy, axisY * velocityEnemy);
        enemyNextCell = enemyCell + new Vector3Int(axisX, axisY, 0);
        changeDirection = false;
        
    }

    private void FixedUpdate()
    {
        Move();
        
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") player.GetComponent<PlayerController>().Killed();
        if (collision.gameObject.tag == "Enemy") Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
    }

    void Move()
    {
        
       
        if (enemyPos != rigidbody.position) enemyPos = rigidbody.position;
        if( enemyCell != tilemap.WorldToCell(enemyPos) ) enemyCell = tilemap.WorldToCell(enemyPos);
        if( enemyNextCell != enemyCell + new Vector3Int(axisX, axisY, 0) ) enemyNextCell = enemyCell + new Vector3Int(axisX, axisY, 0);
        //Debug.Log("Enemy Next Cell: " + enemyNextCell);

        
        if (!IsValidCellToMove(enemyNextCell) )
        {
           // Debug.Log("Direction: " + axisX + " " + axisY);
            if ( axisX > 0 )
            {
                if (enemyPos.x >= ((Vector2)tilemap.GetCellCenterWorld(enemyCell)).x ) changeDirection = true ;
            }
            if (axisX < 0)
            {
               // Debug.Log("Enemy Pos: " + enemyPos);
                //Debug.Log("Center of cell: " + ((Vector2)tilemap.GetCellCenterWorld(enemyCell)));
                if (enemyPos.x <= ((Vector2)tilemap.GetCellCenterWorld(enemyCell)).x) changeDirection = true;
            }
            if (axisY > 0)
            {
                if (enemyPos.y >= ((Vector2)tilemap.GetCellCenterWorld(enemyCell)).y ) changeDirection = true;
            }
            if (axisY < 0)
            {
                if (enemyPos.y <= ((Vector2)tilemap.GetCellCenterWorld(enemyCell)).y ) changeDirection = true;
            }

            if(changeDirection)
            {
                RandomDirection();
                CentrarPos();
                rigidbody.velocity = Vector2.zero;
               // Debug.Log("Invalid Cell To Move");
                changeDirection = false;
            }
         
        }
        else if( rigidbody.velocity == Vector2.zero ) rigidbody.velocity = new Vector2(axisX * velocityEnemy, axisY * velocityEnemy );
//Debug.Log("EnemyCell: " + enemyCell);
    }


    void RandomDirection()
    {
        axisX = Random.Range(-1, 2);
        axisY = Random.Range(-1, 2);

        while (!((axisY == 0 || axisX == 0) && (axisY != axisX)))
        {
            axisX = Random.Range(-1, 2);
            axisY = Random.Range(-1, 2);
        }
    }


    bool IsValidCellToMove(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile || tile == destructibleTile)
        {
            return false;
        }
        foreach( GameObject bomb in GameObject.FindGameObjectsWithTag("Bomb") )
        {
            if (cell == tilemap.WorldToCell(bomb.GetComponent<Transform>().position)) return false;
        }
        return true;
    }

    void CentrarPos()
    {
        enemyPos = gameObject.GetComponent<Transform>().position;
        enemyCell = tilemap.WorldToCell(enemyPos);
        enemyPos = tilemap.GetCellCenterWorld(enemyCell);
        gameObject.transform.position = enemyPos;
    }




}