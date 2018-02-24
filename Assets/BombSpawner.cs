using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour {

    

    public GameObject bombPrefab;

    private Tilemap tilemap;
    private GameObject player;
    private int bombsTogether;

    public bool paused;

    private void Start()
    {
        paused = false;
        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
    }


    // Update is called once per frame
    void Update () {
        

        if ( Input.GetMouseButtonDown(0) && !paused )
        {
            player = GameObject.FindGameObjectWithTag("Player");
            bombsTogether = player.GetComponent<PlayerController>().bombsTogether;
            
            

            if (player != null)
            {
                Vector3 worldPos = player.GetComponent<Transform>().position;
                Vector3Int cell = tilemap.WorldToCell(worldPos);
                Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

                if (GameObject.FindGameObjectsWithTag("Bomb").Length < bombsTogether) Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
            }


        }
	}
}