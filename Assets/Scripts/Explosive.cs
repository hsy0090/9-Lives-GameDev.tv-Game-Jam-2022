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
    [SerializeField]
    GameObject effect;
    [SerializeField]
    GameObject controledeffect;
    [SerializeField]
    GameObject Flash;

    [Header("God Control")]
    [SerializeField]
    string[] comments;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float percentchance = 0.5f;
    bool isControlled = false;

    void Start()
    {
        Timer = TimerInit;
    }

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
            Flash.GetComponent<SpriteRenderer>().enabled = !Flash.GetComponent<SpriteRenderer>().enabled;
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
            if(God.Instance)
            {
                God.Instance.SetText(comments[Random.Range(0, comments.Length)]);
            }

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

            if (God.Instance)
            {
                God.Instance.SetText("Ooh No", true);

                if (Random.value <= percentchance)
                {
                    isControlled = true;
                }
            }
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
                if (God.Instance && !FindObjectOfType<Lives>().deathTag.Contains("Explosive"))
                {
                    God.Instance.SetText("Trying to go out with a bang?", true);
                }

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
