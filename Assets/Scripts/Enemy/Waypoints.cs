using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] points;

    [SerializeField]
    bool repeat = true;

    [Header("Debug")]
    [SerializeField]
    float size = 1.0f;
    [SerializeField]
    Color color = Color.red;

    void Start()
    {
        //Get a waypoints
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        
    }

    public bool GetRepeat()
    {
        return repeat;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; ++i)
        {
            points[i] = transform.GetChild(i);
        }

        for (int j = 0; j < points.Length; ++j)
        {
            Vector3 position = points[j].position;
            if (j > 0)
            {
                Vector3 previous = points[j - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, size);
            }
        }

    }
}
