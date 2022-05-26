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
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(trajectory * BulletSpeed * Time.deltaTime);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }*/
}
