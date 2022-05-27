using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDoor : MonoBehaviour
{
    [SerializeField]
    bool playerinside = false;
    GameObject player;

    [SerializeField]
    GameObject Goto;

    void Start()
    {
        playerinside = false;
    }

    void Update()
    {
        if(playerinside)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = Goto.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            player = collision.gameObject;
            playerinside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            playerinside = false;
        }
    }
}
