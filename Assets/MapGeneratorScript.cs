using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneratorScript : MonoBehaviour {

    
    public TileBase destructable;
    public TileBase wall;
    public GameObject moreBombs, extraLife, moreExplosion, moreVelocity;
    public GameObject goal, enemyGenerator;
    public int numberDestructables = 20;

    private GameObject level;
    private Tilemap tilemap;
    private GameObject currentEnemyGen;


    private int maxY;
    private int minY;
    private int minX;
    private int maxX;
    private int nLevel;
    private int numberPowerUps;


    int x, y;
    Vector3Int enemyGenCell;


 


    public void GenerateNewMap()
    {

        
        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
        

        level = GameObject.FindGameObjectWithTag("Level");

        maxY = level.GetComponent<LevelScript>().maxY;
        maxX = level.GetComponent<LevelScript>().maxX;
        minY = level.GetComponent<LevelScript>().minY;
        minX = level.GetComponent<LevelScript>().minX;

        

        nLevel = level.GetComponent<LevelScript>().level;
        numberPowerUps = nLevel + 1;
        int random;
        GameObject powerup;


        GenerateWalls(nLevel);
        


        for (int i = 0; i < numberDestructables * nLevel && i < 100; i++)
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);

            while (CantPutDestructable(x, y))
            {
                x = Random.Range(minX, maxX);
                y = Random.Range(minY, maxY);
            }

            tilemap.SetTile(new Vector3Int(x, y, 0), destructable); // Destructables
            if (i < numberPowerUps)                                 // PowerUps
            {
                random = Random.Range(0, 4);

                if (random == 0) powerup = moreBombs;
                else if (random == 1) powerup = moreVelocity;
                else if (random == 2) powerup = extraLife;
                else powerup = moreExplosion;
                Instantiate(powerup, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
            }
        }

        while (CantPutEnemyGenerator(x, y) )
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
        }

        currentEnemyGen = Instantiate(enemyGenerator, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
        
        currentEnemyGen.GetComponent<EnemyGeneratorScript>().GenerateEnemies(level.GetComponent<LevelScript>().level);
        enemyGenCell = new Vector3Int(x, y, 0);


        while (CantPutGoal(x, y))
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
        }

        Instantiate(goal, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
        



    }


    public void DestroyOldMap()
    {


        maxY = level.GetComponent<LevelScript>().maxY;
        maxX = level.GetComponent<LevelScript>().maxX;
        minY = level.GetComponent<LevelScript>().minY;
        minX = level.GetComponent<LevelScript>().minX;

        tilemap = GameObject.FindGameObjectWithTag("TilemapGameplay").GetComponent<Tilemap>();
        level = GameObject.FindGameObjectWithTag("Level");

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }


        foreach (GameObject pu in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(pu);
        }

        if (GameObject.FindGameObjectWithTag("Goal") != null) Destroy(GameObject.FindGameObjectWithTag("Goal"));
        if (GameObject.FindGameObjectWithTag("EnemyGenerator") != null) Destroy(GameObject.FindGameObjectWithTag("EnemyGenerator"));
    }



    private void GenerateWalls( int nLevel )
    {
        if( nLevel%10 == 1 )
        {
            for( int i=minX+1; i<=maxX; i+=2 )
            {
                for( int j=minY+1; j<=maxY; j+=2 )
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }
        }
    }





















    bool CantPutDestructable(int x, int y)
    {
        bool cant = false;
        cant = tilemap.GetTile(new Vector3Int(x, y, 0)) == wall || tilemap.GetTile(new Vector3Int(x, y, 0)) == destructable || (x <= minX + 1 && y >= maxY - 1) || (x >= enemyGenCell.x - 1 && x <= enemyGenCell.x + 1 && y >= enemyGenCell.y - 1 && y <= enemyGenCell.y + 1);

        return cant;
    }

    bool CantPutEnemyGenerator(int x, int y)
    {
        bool cant = false;
        cant = tilemap.GetTile(new Vector3Int(x, y, 0)) == wall || (x <= minX + 1 && y >= maxY - 1) || tilemap.GetTile(new Vector3Int(x, y, 0)) == destructable;
        if( !cant )
        {
            if (GameObject.FindGameObjectWithTag("Goal") != null) cant = ( new Vector3Int( x, y, 0 ) == GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position );
        }
        if (!cant)
        {
            if (GameObject.FindGameObjectWithTag("EnemyGenerator") != null) cant = (new Vector3Int(x, y, 0) == GameObject.FindGameObjectWithTag("EnemyGenerator").GetComponent<Transform>().position);
        }

        if (!cant) cant = (x <= 0 && y >= 0);

        return cant;
    }

    bool CantPutGoal(int x, int y)
    {
        bool cant = false;
        cant = tilemap.GetTile(new Vector3Int(x, y, 0)) == wall || tilemap.GetTile(new Vector3Int(x, y, 0)) != destructable || (x <= minX + 1 && y >= maxY - 1) || (x >= enemyGenCell.x - 1 && x <= enemyGenCell.x + 1 && y >= enemyGenCell.y - 1 && y <= enemyGenCell.y + 1);
        if (!cant)
        {
            if (GameObject.FindGameObjectWithTag("Goal") != null) cant = (new Vector3Int(x, y, 0) == GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position);
        }
        if (!cant)
        {
            if (GameObject.FindGameObjectWithTag("EnemyGenerator") != null) cant = (new Vector3Int(x, y, 0) == GameObject.FindGameObjectWithTag("EnemyGenerator").GetComponent<Transform>().position);
        }

        if (!cant) {
            foreach (GameObject powerup in GameObject.FindGameObjectsWithTag("PowerUp"))
            {
                if (tilemap.WorldToCell(powerup.GetComponent<Transform>().position) == new Vector3Int(x, y, 0)) cant = true;
            }
        }

        if (!cant) cant = (x <= 0 && y >= 0);

        return cant;
    }
}
