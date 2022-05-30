using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truckun : MonoBehaviour
{
    [SerializeField]
    Vector3 scalechange;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localScale += scalechange * Time.deltaTime;
    }
}
