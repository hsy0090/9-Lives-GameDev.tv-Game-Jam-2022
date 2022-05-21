using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{

    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------
    bool Fired = false;
    GameObject bullet;
    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    Transform Pivot;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletSpawn;

    [SerializeField]
    Vector3 trajectory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
 //       mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Pivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0) && !Fired)
        {
            Debug.Log("BUTTON PRESSED");
            bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.GetComponentInParent<Transform>().rotation);
            trajectory = new Vector3(1, 0 ,0).normalized;
            bullet.GetComponent<BulletBehavior>().trajectory = trajectory;
            Fired = true;
        }
        else if (Input.GetMouseButtonUp(0) && Fired)
        {
            Fired = false;
        }
    }
}
