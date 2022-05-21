using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game")]
    [SerializeField]
    Transform respawn;

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
    Animator animator;

    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curjumpforce = 0.0f;
        grounded = false;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    #region Input
        if (Input.GetButton("Jump") && grounded)
        {
            curjumpforce = jumpforce;
            rb2d.velocity = Vector2.up * curjumpforce;
        }
        else {
            curjumpforce = 0.0f;
        }

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movespeed, 0, 0);
        #endregion

        animator.SetFloat("X Axis", Input.GetAxis("Horizontal"));
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
        }
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
    }
}
