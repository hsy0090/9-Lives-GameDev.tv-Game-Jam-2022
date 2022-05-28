using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subj;
    Vector2 startpos;
    float startz;

    [SerializeField]
    float yoffset = 1.5f;
    
    Vector2 travel => (Vector2)cam.transform.position - startpos;

    float distancefromsubj => transform.position.z - subj.position.z;
    float clippingPlane => (cam.transform.position.z + (distancefromsubj > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxfactor => Mathf.Abs(distancefromsubj) / clippingPlane;

    // Use this for initialization
    void Start()
    {
        startpos = transform.position;
        startz = transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = startpos + travel * parallaxfactor;
        pos.y += yoffset;
        transform.position = new Vector3(pos.x, pos.y, startz);
    }
}
