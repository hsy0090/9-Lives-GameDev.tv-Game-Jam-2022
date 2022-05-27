using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------
    [System.NonSerialized]
    public float BulletSpeed = 5;

    [System.NonSerialized]
    public Vector3 trajectory;
    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------
    public bool player = false;
    public bool reflected = false;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerProjectiles");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += trajectory * BulletSpeed * Time.deltaTime;
    }
/*    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((((1 << collision.gameObject.layer) & platformLayerMask) != 0))
        Destroy(gameObject);
    }
}
