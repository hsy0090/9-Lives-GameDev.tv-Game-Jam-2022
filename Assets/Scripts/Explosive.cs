using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    GameObject player;
    bool RunTimer = false;
    bool activated = false;
    bool playerinside = false;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float Timer;

    [SerializeField]
    float TimerInit = 2;
    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerInit;
    }

    // Update is called once per frame
    void Update()
    {
        if (RunTimer && !activated)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                RunTimer = false;
                activated = true;
            }
        }
        if (activated)
        {
            if (playerinside)
            {
                FindObjectOfType<Lives>().Death("Explosive");
                
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || 
            (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().onfire))
        {
            StartTimer();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerinside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerinside = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (activated)
        {
            if (collision.gameObject.CompareTag("Player") && gameObject != null)
            {
                FindObjectOfType<Lives>().Death("Explosive");
            }
        }

    }
    public void StartTimer()
    {
        RunTimer = true;
    }
}
