using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum States
    {
        IDLE,
        PATROL,
        ATTACK,
        CONTROLLED
    }
    States state;

    [Header("Movement")]
    [SerializeField]
    float movespeed = 2.5f;

    [SerializeField]
    Waypoints path;
    int pathnodeid = 0;
    Transform target;
    bool back = false;

    [Header("Combat")]
    [SerializeField]
    Transform Pivot;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletSpawn;
    [SerializeField]
    float shootdelay = 5.0f;
    float shoottimer;
    GameObject bullet;

    [Header("God Control")]
    [SerializeField]
    GameObject Controldisplay;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float percentchance = 0.5f;
    bool controlled = false;
    [SerializeField]
    List<Enemy> allies;

    void Start()
    {
        Controldisplay.SetActive(false);

        if (path != null) {
            target = path.points[0];
        }

        state = States.PATROL;
        shoottimer = shootdelay;
    }

    void Update()
    {

        switch (state) {

            case States.IDLE:
                break;

            case States.PATROL:
                if (path != null) {
                    Patrol();
                }
                break;

            case States.ATTACK:
                Attack();
                break;

            case States.CONTROLLED:
                Controlled();
                break;

            default:
                break;

        }
    }

    void Patrol()
    {
        Vector2 dir = target.position - transform.position;

        transform.Translate(dir.normalized * movespeed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void Attack()
    {
        shoottimer -= Time.deltaTime;
        Vector2 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Pivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (shoottimer <= 0.0f) {
            bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.GetComponentInParent<Transform>().rotation);
            
            dir = new Vector3(1, 0, 0).normalized;
            bullet.GetComponent<BulletBehavior>().trajectory = dir;
            
            shoottimer = shootdelay;
        }
    }

    void Controlled()
    {
        Controldisplay.SetActive(true);

        if (!target && allies.Count > 0)
        {
            target = allies[0].gameObject.transform;
        }
        else if (!target && allies.Count <= 0)
            return;

        shoottimer -= Time.deltaTime;

        Vector2 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Pivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (shoottimer <= 0.0f)
        {
            bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.GetComponentInParent<Transform>().rotation);

            dir = new Vector3(1, 0, 0).normalized;
            bullet.GetComponent<BulletBehavior>().trajectory = dir;

            shoottimer = shootdelay;
        }
    }

    void GetNextWaypoint()
    {
        //Reset path if reached end
        if (!back)
        {
            if (pathnodeid >= path.points.Length - 1)
            {
                if (path.GetRepeat())
                {
                    back = true;
                    return;
                }

                pathnodeid = 0;
                target = path.points[pathnodeid];
                return;
            }
        }

        if (!back)
            pathnodeid++;
        else
            pathnodeid--;

        if (back && pathnodeid == 0)
        {
            pathnodeid = 0;
            back = false;
        }

        target = path.points[pathnodeid];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject.tag == "Mob" 
            && !allies.Contains(collision.gameObject.GetComponent<Enemy>()))
        {
            allies.Add(collision.gameObject.GetComponent<Enemy>());
        }
        
        if (collision.gameObject.tag == "Player" && !controlled)
        {
            target = collision.transform;

            state = States.ATTACK;

            if (Random.value <= percentchance)
            {
                controlled = true;
                state = States.CONTROLLED;
                target = null;
                return;
            }
        }

        if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<BulletBehavior>().player)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !controlled)
        {
            if (path != null)
            {
                target = path.points[0];
            }

            state = States.PATROL;
        }
    }
}
