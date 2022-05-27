using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
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
                    Triggered = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    Triggered = true;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<BulletBehavior>().trajectory *= -1;
            collision.gameObject.GetComponent<BulletBehavior>().reflected =  true;
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
            {
                collision.gameObject.layer = LayerMask.NameToLayer("EnemyProjectiles");
                collision.gameObject.GetComponent<BulletBehavior>().player = false;
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
            {
                collision.gameObject.layer = LayerMask.NameToLayer("PlayerProjectiles");
                collision.gameObject.GetComponent<BulletBehavior>().player = true;
            }

        }
    }
}
