using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObj : MonoBehaviour
{
    [System.NonSerialized]
    public List<Vector3> reversepos = new List<Vector3>();
    Rigidbody2D rb2d;

    [System.NonSerialized]
    public bool rewinding = false;

    void Start()
    {
        rewinding = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rewinding)
            rb2d.isKinematic = true;
        else
            rb2d.isKinematic = false;
    }
}
