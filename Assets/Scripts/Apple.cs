using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    
    bool RunTimer = false;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float Timer;

    [SerializeField]
    float TimerInit = 60;

    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerInit;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (RunTimer)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0)
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                RunTimer = false;
            }
        }
    }
    void StartTimer()
    {
        RunTimer = true;
    }
    void ResetTimer()
    {
        Timer = TimerInit;
        RunTimer = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartTimer();
        }
    }
}
