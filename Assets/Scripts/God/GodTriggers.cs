using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodTriggers : MonoBehaviour
{
    [SerializeField]
    string[] text;

    bool playerinside = false;

    [Header("Timer")]
    [SerializeField]
    bool loop = false;
    bool done = false;

    [SerializeField]
    float speechtime = 5.0f;

    [SerializeField]
    float speechtimer;

    void Start()
    {
        playerinside = false;
        speechtimer = speechtime;
        done = false;
    }

    void Update()
    {
        if(loop && playerinside)
        {
            speechtimer -= Time.deltaTime;

            if(speechtimer <= 0.0f)
            {
                if (God.Instance)
                    God.Instance.SetText(text[Random.Range(0, text.Length)]);

                speechtimer = speechtime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !done)
        {
            if (!loop)
                done = true;

            if (God.Instance)
                God.Instance.SetText(text[Random.Range(0, text.Length)]);

            playerinside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerinside = false;
        }
    }
}
