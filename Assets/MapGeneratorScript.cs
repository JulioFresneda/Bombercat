using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneratorScript : MonoBehaviour {

    public Tilemap tilemap;
    public TileBase destructable;
    public TileBase wall;
    public GameObject level;

    public GameObject moreBombs, extraLife, moreExplosion, moreVelocity;

    public GameObject goal;

    private int maxY;
    private int minY;
    private int minX;
    private int maxX;
    private int nLevel;

    public int numberDestructables = 20;
    private int numberPowerUps;


    int x, y;
    Vector3Int enemyGenCell;


    // Use this for initialization
    void Start () {
        
        maxY = level.GetComponent<LevelScript>().maxY;
        maxX = level.GetComponent<LevelScript>().maxX;
        minY = level.GetComponent<LevelScript>().minY;
        minX = level.GetComponent<LevelScript>().minX;
        nLevel = level.GetComponent<LevelScript>().level;
        numberPowerUps = nLevel + 1;
        int random;
        GameObject powerup;
        

        bool goalPost = false;



        enemyGenCell = tilemap.WorldToCell(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>().position);
       

        for ( int i=0; i<numberDestructables*nLevel && i<100; i++ )
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);

            while( CantPutDestructable(x,y)  )
            {
                x = Random.Range(minX, maxX);
                y = Random.Range(minY, maxY);
            }

            tilemap.SetTile(new Vector3Int(x, y, 0), destructable);

            if ((x >= 0 || y <= 0) && !goalPost)
            {
                Instantiate(goal, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
                goalPost = true;
            }

            if (i >= numberDestructables*nLevel-nLevel-1 || i >= 100-nLevel ) {
                random = Random.Range(0, 4);

                if (random == 0) powerup = moreBombs;
                else if (random == 1) powerup = moreVelocity;
                else if (random == 2) powerup = extraLife;
                else powerup = moreExplosion;
                if( goalPost && new Vector3Int(x,y,0) != tilemap.WorldToCell(GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position) ) Instantiate(powerup, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
            }

            if( i == numberDestructables*nLevel-1 || i == 99 )
            {
                if( !goalPost )
                {
                    Instantiate(goal, tilemap.GetCellCenterWorld(new Vector3Int(6, -7, 0)), Quaternion.identity);
                }
            }

            
        }


       

       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool CantPutDestructable( int x, int y )
    {
        bool cant = false;
        cant = tilemap.GetTile(new Vector3Int(x, y, 0)) == wall || tilemap.GetTile(new Vector3Int(x, y, 0)) == destructable || (x <= minX + 1 && y >= maxY - 1) || ( x >= enemyGenCell.x-1 && x<=enemyGenCell.x+1 && y >= enemyGenCell.y - 1 && y <= enemyGenCell.y + 1) ;
 
        return cant;
    }


    public void GenerateNewMap()
    {
        for( int i=minX; i<=maxX; i++ )
        {
            for( int j=minY; i<maxY; i++ )
            {
                if (tilemap.GetTile<Tile>(new Vector3Int(i, j, 0)) == destructable) tilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }


        foreach ( GameObject pu in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(pu);
        }


        nLevel = level.GetComponent<LevelScript>().level;
        numberPowerUps = nLevel + 1;
        int random;
        GameObject powerup;



        enemyGenCell = tilemap.WorldToCell(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>().position);


        for (int i = 0; i < numberDestructables * nLevel && i < 100; i++)
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);

            while (CantPutDestructable(x, y))
            {
                x = Random.Range(minX, maxX);
                y = Random.Range(minY, maxY);
            }

            tilemap.SetTile(new Vector3Int(x, y, 0), destructable);
            if (i < numberPowerUps)
            {
                random = Random.Range(0, 4);

                if (random == 0) powerup = moreBombs;
                else if (random == 1) powerup = moreVelocity;
                else if (random == 2) powerup = extraLife;
                else powerup = moreExplosion;
                Instantiate(powerup, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
            }
        }

        
    }
}
