using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    GameObject top;

    [SerializeField]
    GameObject bot;

    [SerializeField]
    GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if(player.transform.position.y >= -1.0f)
        {
            top.SetActive(true);
            bot.SetActive(false);
        }
        else
        {
            top.SetActive(false);
            bot.SetActive(true);
        }
    }
}
