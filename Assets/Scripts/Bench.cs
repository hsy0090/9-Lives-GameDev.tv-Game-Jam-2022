using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    bool playerinside = false;
    bool Triggered = false;
    GameObject player;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    GameObject affectedObject;

    [SerializeField]
    GameObject TriggerIcon;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerinside)
        {
            if (TriggerIcon.GetComponent<SpriteRenderer>().enabled &&
                    Input.GetKeyDown(KeyCode.E))
            {
                if (Triggered)
                {
                    TriggerOff();
                }
                else
                {
                    TriggerOn();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerIcon.GetComponent<SpriteRenderer>().enabled = true;
            playerinside = true;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerIcon.GetComponent<SpriteRenderer>().enabled = false;
            playerinside = false;
        }

    }
    void TriggerOn()
    {
        Triggered = true;
        affectedObject.GetComponent<Apple>().StartTimer();
        player.GetComponent<PlayerController>().canMove = false;
    }

    void TriggerOff()
    {
        Triggered = false;
        affectedObject.GetComponent<Apple>().ResetTimer();
        player.GetComponent<PlayerController>().canMove = true;
    }
}
