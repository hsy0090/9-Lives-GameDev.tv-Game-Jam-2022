using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    bool activated = false;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    LayerMask platformLayerMask;

    [Header("God Control")]
    [SerializeField]
    string[] comments;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float percentchance = 0.5f;
    bool isControlled = false;

    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void Controlled()
    {
        isControlled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (Random.value <= percentchance) { 
                isControlled = true;
                Destroy(gameObject);

                if(God.Instance)
                {
                    God.Instance.SetText(comments[Random.Range(0, comments.Length)]);
                }
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                activated = true;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
        if (activated && collision.gameObject.CompareTag("Player"))
        {
            if (God.Instance && !FindObjectOfType<Lives>().deathTag.Contains("Stalactite"))
            {
                God.Instance.SetText("Looks like it hurt", true);
            }

            FindObjectOfType<Lives>().Death("Stalactite");
        }
        if (activated && (((1 << collision.gameObject.layer) & platformLayerMask) != 0))
        {
            Destroy(gameObject);
        }

    }
}
