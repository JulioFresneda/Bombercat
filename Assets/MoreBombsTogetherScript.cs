using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBombsTogetherScript : MonoBehaviour
{

    private GameObject level;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    private void PickUp(Collider2D player)
    {
        player.GetComponent<PlayerController>().bombsTogether++;
        level.GetComponent<LevelScript>().score += 5 * level.GetComponent<LevelScript>().level;
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        level = GameObject.FindGameObjectWithTag("Level");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
