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

    public Animator playerAnimator;




    // Use this for initialization
    void Start () {

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
     
    }
	
	// Update is called once per frame
	void Update () {

        Move();
	}

    void Move()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            if (!playerAnimator.GetBool("WalkingLeft"))
            {
                playerAnimator.SetBool("WalkingLeft", true);
                playerAnimator.SetBool("WalkingRight", false);
            }
            rigidbody.velocity = new Vector2(-velocityPlayer, 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!playerAnimator.GetBool("WalkingRight"))
            {
                playerAnimator.SetBool("WalkingRight", true);
                playerAnimator.SetBool("WalkingLeft", false);
            }
            rigidbody.velocity = new Vector2(velocityPlayer, 0);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if( playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleRight") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkingRight"))
            {
                Debug.Log("xd");
                playerAnimator.SetBool("WalkingRight", true);
                playerAnimator.SetBool("WalkingLeft", false);
            }
            else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkingLeft") )
            {
                playerAnimator.SetBool("WalkingRight", false);
                playerAnimator.SetBool("WalkingLeft", true);
            }
            rigidbody.velocity = new Vector2(0, velocityPlayer);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleRight") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkingRight"))
            {
                playerAnimator.SetBool("WalkingRight", true);
                playerAnimator.SetBool("WalkingLeft", false);
            }
            else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkingLeft"))
            {
                playerAnimator.SetBool("WalkingRight", false);
                playerAnimator.SetBool("WalkingLeft", true);
            }
            rigidbody.velocity = new Vector2(0, -velocityPlayer);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            playerAnimator.SetBool("WalkingRight", false);
            playerAnimator.SetBool("WalkingLeft", false);
        }
    }

    public void Default()
    {
       velocityPlayer = 4f;
       explosionRange = 1;
       bombsTogether = 1;

       playerAnimator.SetBool("WalkingRight", false);
       playerAnimator.SetBool("WalkingLeft", false);

        

    }


    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

   







}