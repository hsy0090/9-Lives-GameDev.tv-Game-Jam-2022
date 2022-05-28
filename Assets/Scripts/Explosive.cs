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
    bool isControlled = false;
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
    [SerializeField]
    GameObject effect;
    [SerializeField]
    GameObject controledeffect;
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
        else if (activated && !isControlled)
        {
            if(!effect.GetComponent<ParticleSystem>().isPlaying)
            {
                effect.GetComponent<ParticleSystem>().Play();
            }
            if (effect.GetComponent<ParticleSystem>().time > effect.GetComponent<ParticleSystem>().main.duration /4)
            {
                explode();
                Destroy(gameObject);
            }
        }
        else if(activated && isControlled)
        {
            controledeffect.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || 
            (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().onfire))
        {
            StartTimer();
            
        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void StartTimer()
    {
        RunTimer = true;
    }

    public void Controlled()
    {
        isControlled = true;
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
