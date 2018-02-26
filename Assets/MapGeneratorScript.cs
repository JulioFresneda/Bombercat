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
        int maxRandomTries = 1000;


        GenerateWalls(nLevel);


        for (int i = 0; i < Mathf.Max(15,numberDestructables * nLevel/2) && i < 50; i++)
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);

            while (CantPutDestructable(x, y) && maxRandomTries > 0 )
            {
                x = Random.Range(minX, maxX);
                y = Random.Range(minY, maxY);
                maxRandomTries--;
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
        maxRandomTries = 1000;
        while (CantPutEnemyGenerator(x, y) && maxRandomTries > 0 )
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
            maxRandomTries--;
        }

        currentEnemyGen = Instantiate(enemyGenerator, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
        
        currentEnemyGen.GetComponent<EnemyGeneratorScript>().GenerateEnemies(level.GetComponent<LevelScript>().level);
        enemyGenCell = new Vector3Int(x, y, 0);

        if( tilemap.GetTile(new Vector3Int(x-1,y,0)) == destructable ) tilemap.SetTile(new Vector3Int(x - 1, y, 0), null);
        if (tilemap.GetTile(new Vector3Int(x + 1, y, 0)) == destructable) tilemap.SetTile(new Vector3Int(x + 1, y, 0), null);
        if (tilemap.GetTile(new Vector3Int(x, y - 1, 0)) == destructable) tilemap.SetTile(new Vector3Int(x, y - 1, 0), null);
        if (tilemap.GetTile(new Vector3Int(x, y + 1, 0)) == destructable) tilemap.SetTile(new Vector3Int(x, y + 1, 0), null);

        while (CantPutGoal(x, y))
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
        }

        Instantiate(goal, tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
        



    }


    public void DestroyOldMap()
    {
        foreach( GameObject explosion in GameObject.FindGameObjectsWithTag("Explosion"))
        {
            Destroy(explosion); 
        }

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

        if (nLevel % 10 == 7)
        {
            for (int i = minX + 1; i <= maxX; i += 2)
            {
                for (int j = minY + 1; j <= maxY; j += 2)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }

            for( int i=minX; i<=maxX; i++ )
            {
                if( i > minX ) tilemap.SetTile(new Vector3Int(i, 4, 0), wall);
                if (i > minX) tilemap.SetTile(new Vector3Int(i, -4, 0), wall);
                if (i < maxX) tilemap.SetTile(new Vector3Int(i, 0, 0), wall);
            }
        }

        if (nLevel % 10 == 6)
        {
            for (int i = minX + 1; i <= maxX; i += 2)
            {
                for (int j = minY + 1; j <= maxY; j += 2)
                {
                    if( i != 0 && j != -1 ) tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }

            for (int i = minX; i <= maxX; i++)
            {
                if (i != minX + 2 && i != maxX - 2)
                {
                    tilemap.SetTile(new Vector3Int(i, 0, 0), wall);
                    tilemap.SetTile(new Vector3Int(i, -2, 0), wall);
                }
            }


            for (int i = minY; i <= maxY; i++)
            {
                if (i != minY + 2 && i != maxY - 2)
                {
                    tilemap.SetTile(new Vector3Int(-1, i, 0), wall);
                    tilemap.SetTile(new Vector3Int(1, i, 0), wall);
                }
            }
        }

        if (nLevel % 10 == 5)
        {
            for (int i = minX + 1; i <= maxX; i += 2)
            {
                for (int j = minY + 1; j <= maxY; j += 2)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }

            for( int i=minY; i<=maxY; i++ )
            {
                if( i<maxY ) tilemap.SetTile(new Vector3Int(minX+1, i, 0), wall);
                if( i>minY ) tilemap.SetTile(new Vector3Int(5, i, 0), wall);
            }

            for( int i=minX+2; i<maxX-1; i++ )
            {
                if( i > minX+2) tilemap.SetTile(new Vector3Int(i, -6, 0), wall);
                if( i < maxX-3) tilemap.SetTile(new Vector3Int(i, 4, 0), wall);
            }
        }

        if (nLevel % 10 == 4)
        {

            for( int i=minX+3; i<=minX+9; i++ )
            {
                if( i != minX+4 && i != minX+8 ) tilemap.SetTile(new Vector3Int(i, 4, 0), wall);
            }

            tilemap.SetTile(new Vector3Int(minX+2, 3, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 10, 3, 0), wall);

            tilemap.SetTile(new Vector3Int(minX+1, 2, 0), wall);
            tilemap.SetTile(new Vector3Int(minX+11, 2, 0), wall);

            tilemap.SetTile(new Vector3Int(minX + 1, 1, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 3, 1, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 9, 1, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 11, 1, 0), wall);

            for (int i = minX + 1; i <= minX + 11; i++)
            {
                if (i != minX + 2 && i != minX + 5 && i != minX + 6 && i != minX + 7 && i != minX + 10 ) tilemap.SetTile(new Vector3Int(i, 0, 0), wall);
            }

            for (int i = minX + 1; i <= minX + 11; i++)
            {
                if (i != minX + 2 && i != minX + 6 && i != minX + 10) tilemap.SetTile(new Vector3Int(i, -1, 0), wall);
            }

            tilemap.SetTile(new Vector3Int(minX + 1, -3, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 11, -3, 0), wall);

            tilemap.SetTile(new Vector3Int(minX + 2, -4, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 3, -4, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 9, -4, 0), wall);
            tilemap.SetTile(new Vector3Int(minX + 10,-4, 0), wall);

            for( int i=minX+3; i<=minX+9; i+=2 )
            {
                tilemap.SetTile(new Vector3Int(i, -5, 0), wall);
            }

            for (int i = minX + 3; i <= minX + 9; i ++)
            {
                tilemap.SetTile(new Vector3Int(i, -6, 0), wall);
            }



        }

        if (nLevel % 10 == 3)
        {
            for (int i = minX + 1; i <= maxX; i += 2)
            {
                for (int j = minY + 1; j <= maxY; j += 2)
                {
                    if (i == minX + 1 || i == maxX - 1 || j == minY + 1 || j == maxY - 1) tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }

            for (int i = -3; i <=  3; i++)
            {
                if (i != 0) tilemap.SetTile(new Vector3Int(i, 2, 0), wall);
                if (i != 0) tilemap.SetTile(new Vector3Int(i, -4, 0), wall);
            }
            for (int j = -4; j <= 2; j++)
            {
                if (j != -1) tilemap.SetTile(new Vector3Int(3,j, 0), wall);
                if (j != -1) tilemap.SetTile(new Vector3Int(-3, j, 0), wall);
            }
        }

        if ( nLevel%10 == 2 )
        {
            for( int i=minX+1; i<=maxX; i+=2 )
            {
                for( int j=minY+1; j<=maxY; j+=2 )
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), wall);
                }
            }

            for( int i=minX+1; i<=maxX; i+=4 )
            {
                tilemap.SetTile(new Vector3Int(i, 3, 0), wall);
                tilemap.SetTile(new Vector3Int(i, -5, 0), wall);
                tilemap.SetTile(new Vector3Int(i+2, -1, 0), wall);
            }
        }

        if( nLevel%10 == 1 )
        {
            for (int i = minX + 1; i <= maxX; i += 2)
            {
                for (int j = minY + 1; j <= maxY; j += 2)
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
