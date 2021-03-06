using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game")]
    [SerializeField]
    Transform respawn;

    [System.NonSerialized]
    public bool onfire = false;
    public bool canMove = true;
    float burnTimer = 0;

    public GameObject Teleport1;

    public GameObject Teleport2;

    public GameObject Teleport3;

    [SerializeField]
    float burnTick = 1;
    [SerializeField]
    GameObject fire;

    [SerializeField]
    GameObject health;

    [SerializeField]
    GameObject life;

    [SerializeField]
    float idleTimer;

    [SerializeField]
    float idleDeathTime = 60;

    [Header("God Heal")]
    [SerializeField]
    string[] comments;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float percentchance = 0.5f;
    [SerializeField]
    GameObject heal;

    [Header("Movement")]
    [SerializeField]
    float movespeed = 2.5f;

    [Header("Jump")]
    [SerializeField]
    float jumpforce = 6.0f;
    float curjumpforce;

    [SerializeField]
    LayerMask platformLayerMask;
    public bool grounded;
    Rigidbody2D rb2d;

    [SerializeField]
    int playerlayer;
    [SerializeField]
    int platformlayer;
    [SerializeField]
    bool jumping = false;

    [Header("Gun Control")]
    [SerializeField]
    GameObject gun;

    [SerializeField]
    Vector3 playerpos;

    Animator animator;


    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curjumpforce = 0.0f;
        grounded = false;
        fire.SetActive(false);
        playerpos = transform.position;
        idleTimer = idleDeathTime;
        heal.SetActive(false);
    }

    void Update()
    {
        if (grounded)
            animator.SetBool("Falling", false);

        //test fire
        if (Input.GetKeyDown(KeyCode.J))
        {
            onfire = true;
            fire.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Heal();
        }

        if (canMove && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Input.GetButtonDown("Jump") && !jumping)
        {
            StartCoroutine("Jump");
        }
        else if (canMove && Input.GetButton("Jump") && grounded && !jumping)
        {
            curjumpforce = jumpforce;
            rb2d.velocity = Vector2.up * curjumpforce;
            StartCoroutine("Jump");
            animator.SetBool("Jumping", true);
        }
        else
        {
            curjumpforce = 0.0f;
        }

        if (playerpos == transform.position && !life.GetComponent<Lives>().deathTag.Contains("Bored to Death"))
        {
            idleTimer -= Time.deltaTime;

            if (God.Instance)
            {
                God.Instance.RandomLines();
            }

            if (idleTimer <= 0)
            {
                if (God.Instance && !FindObjectOfType<Lives>().deathTag.Contains("Bored to Death"))
                {
                    God.Instance.SetText("Here's a free one for you for making me bored.", true);
                }

                life.GetComponent<Lives>().Death("Bored to Death");
            }
        }
        else
        {
            playerpos = transform.position;
            idleTimer = idleDeathTime;

            if (God.Instance)
            {
                God.Instance.ResetRandLines();
            }
        }

        if (onfire)
        {
            burnTimer += Time.deltaTime;
            if (burnTimer >= burnTick)
            {
                health.GetComponent<Health>().dealDamage(1, "Fire");
                burnTimer = 0;

                if (health.GetComponent<Health>().health == 1 && Random.value <= percentchance)
                {
                    Heal();
                }
            }
        }
        else
        {
            burnTimer = 0;
        }

        if (rb2d.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, true);

        else if (rb2d.velocity.y <= 0 && !jumping)
        {
            Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, false);
            animator.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {

        #region Input

        if (canMove)
        {
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movespeed, 0, 0);
            animator.SetBool("Sitting", false);
        }
        else
        {
            animator.SetBool("Sitting", true);
        }

        #endregion

        animator.SetFloat("X Axis", Input.GetAxis("Horizontal"));
        animator.SetFloat("JumpForce", curjumpforce);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")) * 10);

        if (Input.GetAxis("Horizontal") > 0 && canMove)
        {
            animator.SetBool("LastFacingRight", true);
            animator.SetBool("LastFacingLeft", false);
        }
        else if (Input.GetAxis("Horizontal") < 0 && canMove)
        {
            animator.SetBool("LastFacingRight", false);
            animator.SetBool("LastFacingLeft", true);
        }

        if (grounded)
            animator.SetBool("Falling", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !collision.gameObject.GetComponent<BulletBehavior>().player)
        {
            if (collision.gameObject.GetComponent<BulletBehavior>().reflected)
            {
                health.GetComponent<Health>().dealDamage(5, "Reflected Bullet");
            }
            else
            {
                health.GetComponent<Health>().dealDamage(5, "Bullet");
            }

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fire Bullet") && !collision.gameObject.GetComponent<BulletBehavior>().player)
        {
            if (collision.gameObject.GetComponent<BulletBehavior>().reflected)
            {
                health.GetComponent<Health>().dealDamage(0, "Reflected Bullet");
            }
            else
            {
                health.GetComponent<Health>().dealDamage(0, "Bullet");
            }
            onfire = true;
            fire.SetActive(true);
            Destroy(collision.gameObject);
        }

        grounded = collision != null && (((1 << collision.gameObject.layer) & platformLayerMask) != 0);
        
        if (grounded)
            animator.SetBool("Falling", false);

        if (!jumping)
            animator.SetBool("Jumping", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grounded = collision != null && (((1 << collision.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        animator.SetBool("Falling", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Killzone")
        {
            transform.position = respawn.transform.position;

            if (God.Instance)
                God.Instance.SetText("Ooh I don't think so", true);
        }

        if (collision.gameObject.tag == "Water")
        {
            onfire = false;
            fire.SetActive(false);
        }

        if (collision.gameObject.tag == "Truck")
        {
            FindObjectOfType<Lives>().Death("TruckKun");
        }
    }

    IEnumerator Jump()
    {
        jumping = true;
        Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, true);
        yield return new WaitForSeconds(1.0f);
        Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, false);
        jumping = false;
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", true);
    }

    public void Respawn()
    {
        transform.position = respawn.transform.position;
        canMove = true;
        onfire = false;
        fire.SetActive(false);
        gun.GetComponent<GunControl>().Reload();
        health.GetComponent<Health>().health = health.GetComponent<Health>().numOfHearts;
        animator.SetBool("Sitting", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Jumping", false);
        if (life.GetComponent<Lives>().life == 1 && GameManager.Instance && GameManager.Instance.GetCurrentSceneName() != "OutroScene")
        {
            GameManager.Instance.ChangeState("OutroScene");
        }
        else
        {
            GameManager.Instance.ChangeState("MainScene");
        }
    }

    void Heal()
    {
        God.Instance.SetText(comments[Random.Range(0, comments.Length)]);
        StartCoroutine(Healing());
        health.GetComponent<Health>().dealDamage(-5, "Heal");
        onfire = false;
        fire.SetActive(false);
    }
    public void Resetsave()
    {
        FindObjectOfType<PlayerSave>().resetsave();
    }

    IEnumerator Healing()
    {
        heal.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        heal.SetActive(false);
    }
}
