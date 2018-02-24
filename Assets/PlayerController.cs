using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {



    public float velocityPlayer = 4f;
    public int explosionRange = 1;
    public int bombsTogether = 1;

    public Tile wallTile;
    public Tile destructibleTile;

    private new Rigidbody2D rigidbody;
    private Tilemap tilemap;





    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();        
    }
	
	// Update is called once per frame
	void Update () {
        Move();
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

    public void Default()
    {
       velocityPlayer = 4f;
       explosionRange = 1;
       bombsTogether = 1;
    }


    

  
}