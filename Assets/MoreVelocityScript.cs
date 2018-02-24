using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreVelocityScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    private void PickUp(Collider2D player)
    {
        player.GetComponent<PlayerController>().velocityPlayer += 2.0f;
        player.GetComponent<PlayerController>().score += 5 * player.GetComponent<PlayerController>().level;
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
