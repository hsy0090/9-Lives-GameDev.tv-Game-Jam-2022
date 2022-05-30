using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    [SerializeField]
    GameObject platform;
    [SerializeField]
    GameObject player;
    [SerializeField]
    int playerprevlayer;
    [SerializeField]
    GameObject gun;
    [SerializeField]
    int gunprevlayer;

    public bool view = false;

    void Start()
    {
        view = false;
        platform.SetActive(false);
        playerprevlayer = player.GetComponent<SpriteRenderer>().sortingOrder;

        if(gun != null)
            gunprevlayer = gun.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            view = true;
            platform.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            platform.SetActive(false);
        }
    }

    public void HidePlayerLayer()
    {
        player.GetComponent<SpriteRenderer>().sortingOrder = -5;

        if (gun != null)
            gun.GetComponent<SpriteRenderer>().sortingOrder = -4;
    }

    public void ResetPlayerlayer()
    {
        player.GetComponent<SpriteRenderer>().sortingOrder = playerprevlayer;

        if (gun != null)
            gun.GetComponent<SpriteRenderer>().sortingOrder = gunprevlayer;
    }
}
