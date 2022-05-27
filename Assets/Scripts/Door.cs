using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<BulletBehavior>().trajectory *= -1;
            collision.gameObject.GetComponent<BulletBehavior>().reflected =  true;
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
    }
}
