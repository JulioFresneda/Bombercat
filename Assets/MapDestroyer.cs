using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour {

    private Tilemap tilemap;

    public Tile wallTile;
    public Tile destructibleTile;

    private GameObject player;
    private GameObject level;

    public GameObject explosionPrefab;

    

    public void Explode( Vector2 worldPos )
    {
        player = GameObject.FindGameObjectWithTag("Player");
        level = GameObject.FindGameObjectWithTag("Level");
        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();

        Vector3Int originalCell = tilemap.WorldToCell(worldPos);
        int explosionRange = player.GetComponent<PlayerController>().explosionRange;

        ExplodeCell(originalCell);

        for (int i = 1; i <= explosionRange && ExplodeCell(originalCell + new Vector3Int(i, 0, 0)); i++ ){ }
        for (int i = 1; i <= explosionRange && ExplodeCell(originalCell + new Vector3Int(-i, 0, 0)); i++) { }
        for (int i = 1; i <= explosionRange && ExplodeCell(originalCell + new Vector3Int(0, i, 0)); i++) { }
        for (int i = 1; i <= explosionRange && ExplodeCell(originalCell + new Vector3Int(0, -i, 0)); i++) { }


    }

    bool ExplodeCell( Vector3Int cell )
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if( tile == wallTile)
        {
            return false;
        }

        foreach (GameObject powerup in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            if (cell == tilemap.WorldToCell(powerup.GetComponent<Transform>().position) && !tilemap.HasTile(cell))
            {
                Destroy(powerup);
            }
        }

        

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        
        if( cell == tilemap.WorldToCell(player.GetComponent<Transform>().position))
        {
            level.GetComponent<LevelScript>().Killed();
        }

        foreach( GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy") )
        {
            if (cell == tilemap.WorldToCell(enemy.GetComponent<Transform>().position))
            {
                level.GetComponent<LevelScript>().score += 10 * level.GetComponent<LevelScript>().level;
                Destroy(enemy);
            }
        }

        if (tile == destructibleTile)
        {
            level.GetComponent<LevelScript>().score++;
            tilemap.SetTile(cell, null);
            return false;

        }





        return true;
    }
}