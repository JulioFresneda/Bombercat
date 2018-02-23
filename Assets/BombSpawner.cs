using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour {

    public Tilemap tilemap;

    public GameObject bombPrefab;

    public GameObject player;
    private int bombsTogether;

	// Update is called once per frame
	void Update () {
        bombsTogether = player.GetComponent<PlayerController>().bombsTogether;

        if ( Input.GetMouseButtonDown(0) )
        {

            Vector3 worldPos = player.GetComponent<Transform>().position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

            if( GameObject.FindGameObjectsWithTag("Bomb").Length < bombsTogether ) Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);


        }
	}
}