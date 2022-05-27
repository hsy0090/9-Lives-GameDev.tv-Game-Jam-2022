using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField]
    ParticleSystem lightning;
    [SerializeField]
    bool lit;
    [SerializeField]
    float lightninginterval = 13.0f;

    [SerializeField]
    float littime = 5.0f;
    float timer;

    bool playerinside = false;

    void Start()
    {
        lightning.Play();
        lit = true;
        timer = littime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (lit)
        {
            if(playerinside)
            {
                FindObjectOfType<Lives>().Death("Lightning");
                playerinside = false;
            }

            if (timer <= 0)
            {
                lit = false;
                timer = lightninginterval;
            }
        }
        else {
            if (timer <= 0)
            {
                lightning.Play();
                timer = littime;
                lit = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lightning")
        {
            lit = true;
            timer = littime;
        }

        if(collision.gameObject.tag == "Player")
        {
            playerinside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerinside = false;
        }
    }
}
