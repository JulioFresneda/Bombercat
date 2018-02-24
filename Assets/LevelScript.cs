using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public int maxY = 5;
    public int minY = -7;
    public int minX = -6;
    public int maxX = 6;

    public bool win = false;
    public int score;
    public int level = 0;

    public GameObject mapGenerator;

  

    // Use this for initialization
    void Start () {
        level = 1;
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Win()
    {
        win = true;
        score += 100 * level;
        level++;

        mapGenerator.GetComponent<MapGeneratorScript>().GenerateNewMap();
    }

   
}
