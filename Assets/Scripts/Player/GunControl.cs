using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField]
    Animator barrel;

    void Start()
    {
        Reload();
    }

    void Update()
    {
        if (FindObjectOfType<GameManager>().isPause() && !EventSystem.current.IsPointerOverGameObject())
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

                    barrel.Play("Shoot");

                    Controller.GetComponent<AmmoControl>().Magazine[Controller.GetComponent<AmmoControl>().currentSlot] = null;
                    Controller.GetComponent<AmmoControl>().MagazineSlot[Controller.GetComponent<AmmoControl>()
                        .currentSlot].GetComponent<Image>().enabled = false;
                    //RemoveAt(Controller.GetComponent<AmmoControl>().currentSlot);

                    trajectory = mousePos.normalized;
                    bullet.GetComponent<BulletBehavior>().trajectory = trajectory;
                    bullet.GetComponent<BulletBehavior>().player = true;
                    Fired = true;
                }
                Controller.GetComponent<AmmoControl>().currentSlot++;
                Controller.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Controller.transform.rotation.eulerAngles.z - 60));
            }
            else if (Input.GetMouseButtonUp(0) && Fired)
            {
                Fired = false;
                barrel.StopPlayback();
            }
        }
    }

    public void Reload()
    {
        for (int i = 0; i < 6; i++)
        {
            if (Controller.GetComponent<AmmoControl>().Magazine[i] == null)
            {
                Controller.GetComponent<AmmoControl>().MagazineSlot[i].GetComponent<Image>().enabled = true;
                Controller.GetComponent<AmmoControl>().Magazine[i] = bulletPrefab;
                Controller.GetComponent<AmmoControl>().currentSlot = 0;
                Controller.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

    }
}
