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

    bool playerinside = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerinside && TeleportActivated)
        {
            if (UpTeleportIcon.GetComponent<SpriteRenderer>().enabled &&
                    (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) &&
                    UpTeleporter.GetComponent<Teleporter>().GetStatus())
            {
                Debug.Log("teleport up");
                player.transform.position = UpTeleporter.transform.position;
                UpTeleporter.GetComponent<ParticleSystem>().Play();
            }
            if (DownTeleportIcon.GetComponent<SpriteRenderer>().enabled &&
                (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) &&
                DownTeleporter.GetComponent<Teleporter>().GetStatus())
            {
                Debug.Log("teleport down");
                player.transform.position = DownTeleporter.transform.position;
                DownTeleporter.GetComponent<ParticleSystem>().Play();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!TeleportActivated)
            {
                TeleportActivated = true;
            }
            if (UpTeleporter != null && UpTeleporter.GetComponent<Teleporter>().GetStatus())
            {
                UpTeleportIcon.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (DownTeleporter != null && DownTeleporter.GetComponent<Teleporter>().GetStatus())
            {
                DownTeleportIcon.GetComponent<SpriteRenderer>().enabled = true;
            }

            player = collision.gameObject;
            playerinside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpTeleportIcon.GetComponent<SpriteRenderer>().enabled = false;
            DownTeleportIcon.GetComponent<SpriteRenderer>().enabled = false;
            playerinside = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    public bool GetStatus()
    {
        return TeleportActivated;
    }
    public void ActivatePortal()
    {
        TeleportActivated = true;
    }

}
