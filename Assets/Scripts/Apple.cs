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
    bool activated = false;
    bool RunTimer = false;
    int count = 0;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float Timer;

    [SerializeField]
    float TimerInit = 5;

    [SerializeField]
    LayerMask platformLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerInit;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (RunTimer && !activated)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0)
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                RunTimer = false;
                activated = true;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
    public void StartTimer()
    {
        RunTimer = true;
    }
    public void ResetTimer()
    {
        Timer = TimerInit;
        RunTimer = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartTimer();
        }
        if (activated && collision.gameObject.CompareTag("Player"))
        {
            if (God.Instance && !FindObjectOfType<Lives>().deathTag.Contains("Apple"))
            {
                God.Instance.SetText("Reached enlightenment yet? hehe.", true);
            }

            FindObjectOfType<Lives>().Death("Apple");
        }
        if (activated && (((1 << collision.gameObject.layer) & platformLayerMask) != 0))
        {

            if (count > 2)
            {
                Destroy(gameObject);
            }
            count += 1;
        }
    }
}
