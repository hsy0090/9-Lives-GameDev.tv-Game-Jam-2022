using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (God.Instance)
            {
                God.Instance.ReverseTime();
            }
        }
    }
}
