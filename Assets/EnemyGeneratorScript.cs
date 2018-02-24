using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorScript : MonoBehaviour {

    public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateEnemies( int number )
    {
        for( int i=0; i<number; i++ )
        {
            Instantiate(enemy, gameObject.GetComponent<Transform>().position, Quaternion.identity);
        }
    }
}
