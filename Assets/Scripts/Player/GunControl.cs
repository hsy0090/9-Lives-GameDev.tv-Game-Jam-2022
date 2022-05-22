using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    GameObject Controller;

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

        if (Input.GetMouseButtonDown(0) && !Fired && (Controller.GetComponent<AmmoControl>().Magazine.Count > 0))
        {
            Debug.Log("BUTTON PRESSED");
            if (Controller.GetComponent<AmmoControl>().Magazine[Controller.GetComponent<AmmoControl>().currentSlot] != null)
            {
                bullet = Instantiate(Controller.GetComponent<AmmoControl>().Magazine[Controller.GetComponent<AmmoControl>().currentSlot]
                    , bulletSpawn.transform.position, bulletSpawn.GetComponentInParent<Transform>().rotation);

                Controller.GetComponent<AmmoControl>().Magazine[Controller.GetComponent<AmmoControl>().currentSlot] = null;
                Controller.GetComponent<AmmoControl>().MagazineSlot[Controller.GetComponent<AmmoControl>()
                    .currentSlot].GetComponent<Image>().enabled = false;
                //RemoveAt(Controller.GetComponent<AmmoControl>().currentSlot);

                trajectory = new Vector3(1, 0, 0).normalized;
                bullet.GetComponent<BulletBehavior>().trajectory = trajectory;
                
                Fired = true;
            }
            Controller.GetComponent<AmmoControl>().currentSlot++;
            Controller.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Controller.transform.rotation.eulerAngles.z - 60));
        }
        else if (Input.GetMouseButtonUp(0) && Fired)
        {
            Fired = false;
        }
    }
}
