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
    [SerializeField]
    GameObject fire;

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

    [SerializeField]
    GameObject health;

    [SerializeField]
    GameObject life;

    Animator animator;

    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curjumpforce = 0.0f;
        grounded = false;
        fire.SetActive(false);
    }

    void Update()
    {
        
        //test fire
        if(Input.GetKeyDown(KeyCode.J))
        {
            onfire = true;
            fire.SetActive(true);
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Input.GetButtonDown("Jump") && !jumping)
        {
            StartCoroutine("Jump");
        }
        else if (Input.GetButton("Jump") && grounded && !jumping)
        {
            curjumpforce = jumpforce;
            rb2d.velocity = Vector2.up * curjumpforce;
            StartCoroutine("Jump");
        }
        else
        {
            curjumpforce = 0.0f;
        }

        if (rb2d.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, true);

        else if (rb2d.velocity.y <= 0 && !jumping)
            Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, false);
    }

    private void FixedUpdate()
    {

        #region Input
       

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movespeed, 0, 0);
        #endregion

/*        animator.SetFloat("X Axis", Input.GetAxis("Horizontal"));
        animator.SetFloat("JumpForce", curjumpforce);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")) * 10);

        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("LastFacingRight", true);
            animator.SetBool("LastFacingLeft", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("LastFacingRight", false);
            animator.SetBool("LastFacingLeft", true);
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grounded = collision != null && (((1 << collision.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Killzone")
        {
            transform.position = respawn.transform.position;

            if(God.Instance)
                God.Instance.SetText("Ooh I don't think so");
        }

        if(collision.gameObject.tag == "Water")
        {
            onfire = false;
            fire.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health.GetComponent<Health>().dealDamage(5, collision.gameObject.tag);
        }
    }

    IEnumerator Jump()
    {
        jumping = true;
        Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, true);
        yield return new WaitForSeconds(1.0f);
        Physics2D.IgnoreLayerCollision(playerlayer, platformlayer, false);
        jumping = false;
    }

    public void Respawn()
    {
        transform.position = respawn.transform.position;
    }
}
