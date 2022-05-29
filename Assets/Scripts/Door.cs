using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    bool playerinside = false;
    bool Triggered = false;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    GameObject TriggerIcon;

    [SerializeField]
    Sprite closed;

    [SerializeField]
    Sprite open;

    [Header("God Control")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float percentchance = 0.5f;
    [SerializeField]
    string[] comments;
    [SerializeField]
    bool isControlled = false;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = closed;
    }

    void Update()
    {
        if (playerinside)
        {
            if (TriggerIcon.GetComponent<SpriteRenderer>().enabled &&
                    Input.GetKeyDown(KeyCode.E))
            {
                if (Triggered)
                {
                    Triggered = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    GetComponent<SpriteRenderer>().sprite = closed;
                }
                else
                {
                    Triggered = true;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().sprite = open;
                }
            }
        }
    }
    public void Controlled()
    {
        isControlled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerIcon.GetComponent<SpriteRenderer>().enabled = true;
            playerinside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerIcon.GetComponent<SpriteRenderer>().enabled = false;
            playerinside = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!isControlled && Random.value <= percentchance)
            {
                isControlled = true;
                GetComponent<SpriteRenderer>().color = Color.red;

                if (God.Instance)
                {
                    God.Instance.SetText(comments[Random.Range(0, comments.Length)]);
                }
            }

                if (!isControlled)
            {
                collision.gameObject.GetComponent<BulletBehavior>().trajectory *= -1;
                collision.gameObject.GetComponent<BulletBehavior>().reflected = true;
                if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("EnemyProjectiles");
                    collision.gameObject.GetComponent<BulletBehavior>().player = false;
                }
                else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("PlayerProjectiles");
                    collision.gameObject.GetComponent<BulletBehavior>().player = true;
                }

            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

    }
}
