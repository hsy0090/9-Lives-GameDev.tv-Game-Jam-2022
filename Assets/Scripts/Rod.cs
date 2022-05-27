using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField]
    bool lit;

    float littime = 5.0f;
    float timer;

    void Start()
    {
        lit = false;
    }

    void Update()
    {
        
        if(lit)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                lit = false;
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

        if(collision.gameObject.tag == "Player" && lit)
        {
            FindObjectOfType<Lives>().Death("Lightning");
        }
    }
}
