using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    bool TeleportActivated = false;

    [SerializeField]
    GameObject UpTeleporter;
    
    [SerializeField]
    GameObject UpTeleportIcon;

    [SerializeField]
    GameObject DownTeleporter;

    [SerializeField]
    GameObject DownTeleportIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!TeleportActivated)
            {
                TeleportActivated = true;
            }
            if (UpTeleporter != null)
            {
                UpTeleportIcon.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (DownTeleporter != null)
            {
                DownTeleportIcon.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpTeleportIcon.GetComponent<SpriteRenderer>().enabled = false;
            DownTeleportIcon.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TeleportActivated)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Active");
                if (UpTeleportIcon.GetComponent<SpriteRenderer>().enabled && 
                    Input.GetKeyDown(KeyCode.UpArrow) && 
                    UpTeleporter.GetComponent<Teleporter>().GetStatus())
                {
                    Debug.Log("teleport up");
                    collision.gameObject.transform.position = UpTeleporter.transform.position;
                }
                if (DownTeleportIcon.GetComponent<SpriteRenderer>().enabled && 
                    Input.GetKeyDown(KeyCode.DownArrow) && 
                    DownTeleporter.GetComponent<Teleporter>().GetStatus())
                {
                    Debug.Log("teleport down");
                    collision.gameObject.transform.position = DownTeleporter.transform.position;
                }
            }
        }
    }
    bool GetStatus()
    {
        return TeleportActivated;
    }

}
