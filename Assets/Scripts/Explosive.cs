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
    bool RunTimer = false;
    bool activated = false;
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
    [SerializeField]
    float fieldOfImpact;
    [SerializeField]
    LayerMask LayerToHit;

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
                explode();
                Destroy(gameObject);
            }
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

    public void StartTimer()
    {
        RunTimer = true;
    }
    void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);
        foreach(Collider2D obj in objects)
        {
            if (obj.gameObject.CompareTag("Player") && gameObject != null)
            {
                FindObjectOfType<Lives>().Death("Explosive");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
