using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;

public class LevelScript : MonoBehaviour {

    public int maxY = 5;
    public int minY = -7;
    public int minX = -6;
    public int maxX = 6;


    public bool win = false;
    public int score;
    public int level;

    public GameObject mapGenerator;

    public int lifes;


    public GameObject c1player, c2player, c3player, c4player, c5player;
    public Tilemap tilemap;



    private Vector3 respawn;
    private bool dying;
    private GameObject player;


    // Use this for initialization
    void Start () {
        dying = false;
        level = 1;
        score = 0;
        lifes = 3;
        Debug.Log("xx");
        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
        respawn = tilemap.GetCellCenterWorld(new Vector3Int(-6, 5, 0));

        if (PlayerPrefs.GetInt("character") == 1)
        {
            player = Instantiate(c1player, respawn, Quaternion.identity);
            Debug.Log("Create c1");
        }
        else if (PlayerPrefs.GetInt("character") == 2)
        {
            player = Instantiate(c2player, respawn, Quaternion.identity);
            Debug.Log("Create c2");
        }
        else if (PlayerPrefs.GetInt("character") == 3)
        {
            player = Instantiate(c3player, respawn, Quaternion.identity);
            Debug.Log("Create c3");
        }
        else if (PlayerPrefs.GetInt("character") == 4)
        {
            player = Instantiate(c4player, respawn, Quaternion.identity);
            Debug.Log("Create c4");
        }
        else if (PlayerPrefs.GetInt("character") == 5)
        {
            player = Instantiate(c5player, respawn, Quaternion.identity);
            Debug.Log("Create c5");
        }
        else player = Instantiate(c1player, respawn, Quaternion.identity);

        mapGenerator.GetComponent<MapGeneratorScript>().GenerateNewMap();
    }
	
	// Update is called once per frame
	void Update () {
        if (InGoal() && !win) Win();
        
    }

    public void Win()
    {
        win = true;
        score += 100 * level;
        level++;
        if (level == 2) PlayerPrefs.SetInt("c2u", 1);

        player.GetComponent<Transform>().position = respawn;

        mapGenerator.GetComponent<MapGeneratorScript>().DestroyOldMap();
        mapGenerator.GetComponent<MapGeneratorScript>().GenerateNewMap();
        win = false;
    }

    public void Killed()
    {
        if (lifes > 1 && !dying )
        {
            dying = true;
            lifes--;

     
            player.GetComponent<Transform>().position = respawn;
            player.GetComponent<PlayerController>().Default();


            Wait(0.5f);
            dying = false;
        }
        else
        {

            int hs1, hs2, hs3;
    

            hs1 = PlayerPrefs.GetInt("hs1o");
            hs2 = PlayerPrefs.GetInt("hs2o");
            hs3 = PlayerPrefs.GetInt("hs3o");


 

            if (score > hs1)
            {

                PlayerPrefs.SetString("nick3o", String.Copy(PlayerPrefs.GetString("nick2o")));
                PlayerPrefs.SetString("nick2o", String.Copy(PlayerPrefs.GetString("nick1o")));
                PlayerPrefs.SetString("nick1o", PlayerPrefs.GetString("nick"));


                PlayerPrefs.SetInt("hs3o", hs2);
                PlayerPrefs.SetInt("hs2o", hs1);
                PlayerPrefs.SetInt("hs1o", score);
            }
            else if (score > hs2)
            {
                PlayerPrefs.SetString("nick3o", String.Copy(PlayerPrefs.GetString("nick2o")));
                PlayerPrefs.SetString("nick2o", String.Copy(PlayerPrefs.GetString("nick")));

                PlayerPrefs.SetInt("hs3o", hs2);
                PlayerPrefs.SetInt("hs2o", score);

            }
            else if (score > hs3)
            {
                PlayerPrefs.SetString("nick3o", String.Copy(PlayerPrefs.GetString("nick")));

                PlayerPrefs.SetInt("hs3o", score);
            }

            PlayerPrefs.SetInt("fs", score);
            PlayerPrefs.SetInt("fl", level);
            SceneManager.LoadScene("GameOver");

            
        }
    }

    bool InGoal()
    {
        bool ingoal = false;

        if (tilemap.WorldToCell(GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position) == tilemap.WorldToCell(player.GetComponent<Transform>().position))
        {
            if( GameObject.FindGameObjectsWithTag("Enemy").Length == 0 ) ingoal = true;
        }

        return ingoal;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }




}
