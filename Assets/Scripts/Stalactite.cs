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
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            activated = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (activated && collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Lives>().Death("Stalactite");
        }
        if (activated && (((1 << collision.gameObject.layer) & platformLayerMask) != 0))
        {
            Destroy(gameObject);
        }

    }
}
