using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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



    // Use this for initialization
    void Start () {
        level = 1;
        score = 0;
        lifes = 3;

        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
        respawn = tilemap.GetCellCenterWorld(new Vector3Int(-6, 5, 0));

        if (player == null) player = Instantiate(player, respawn, Quaternion.identity);
        
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

        mapGenerator.GetComponent<MapGeneratorScript>().GenerateNewMap();
    }

    public void Killed()
    {
        if (lifes > 1)
        {
            lifes--;
            gameObject.GetComponent<Transform>().position = respawn;
            player.GetComponent<PlayerController>().Default();
        }
        // else GameOver
    }

    bool InGoal()
    {
        bool ingoal = false;

        if (tilemap.WorldToCell(GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position) == tilemap.WorldToCell(player.GetComponent<Transform>().position))
        {
            ingoal = true;
        }

        return ingoal;
    }


}
