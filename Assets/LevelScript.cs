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


    public GameObject player;
    public Tilemap tilemap;



    private Vector3 respawn;
    private bool dying;


    // Use this for initialization
    void Start () {
        dying = false;
        level = 1;
        score = 0;
        lifes = 3;
        Debug.Log("xx");
        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
        respawn = tilemap.GetCellCenterWorld(new Vector3Int(-6, 5, 0));

        player = Instantiate(player, respawn, Quaternion.identity);
    
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

            SceneManager.LoadScene("Menu");

            
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
