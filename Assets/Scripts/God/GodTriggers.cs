using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodTriggers : MonoBehaviour
{
    [SerializeField]
    string[] text;

    [SerializeField]
    bool playerinside = false;

    [Header("Timer")]
    [SerializeField]
    bool loop = false;
    bool done = false;

    [SerializeField]
    float speechtime = 5.0f;

    [SerializeField]
    float speechtimer;

    [SerializeField]
    bool queue = false;

    int id = 0;

    void Start()
    {
        playerinside = false;
        speechtimer = speechtime;
        done = false;
        id = 0;
    }

    void Update()
    {
        if(loop && playerinside)
        {
            speechtimer -= Time.deltaTime;

            if(speechtimer <= 0.0f)
            {
                if (God.Instance && !queue)
                    God.Instance.SetText(text[Random.Range(0, text.Length)]);
                else
                {
                    if (God.Instance && id < text.Length && !God.Instance.displaying)
                    {
                        God.Instance.SetText(text[id]);
                        id++;
                    }
                }

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

            if (God.Instance && !queue)
                God.Instance.SetText(text[Random.Range(0, text.Length)]);
            else
            {
                God.Instance.SetText(text[id]);
                id++;
            }

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
