using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float movespeed = 2.5f;
    Rigidbody2D rb2d;
    bool turn = false;

    [SerializeField]
    Waypoints path;
    int pathnodeid = 0;
    Transform target;
    bool back = false;

    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();

        if (path != null) {
            target = path.points[0];
        }
    }

    void Update()
    {
        if (path != null) {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
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
}
