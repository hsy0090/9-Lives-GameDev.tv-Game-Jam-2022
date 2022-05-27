using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDoor : MonoBehaviour
{
    [SerializeField]
    bool playerinside = false;
    GameObject player;

    [SerializeField]
    GameObject goTo;

    [SerializeField]
    GameObject triggerIcon;

    void Start()
    {
        playerinside = false;
        triggerIcon.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(playerinside)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = goTo.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            triggerIcon.GetComponent<SpriteRenderer>().enabled = true;
            player = collision.gameObject;
            playerinside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerIcon.GetComponent<SpriteRenderer>().enabled = false;
            player = null;
            playerinside = false;
        }
    }
}
