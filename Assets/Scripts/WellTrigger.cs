using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellTrigger : MonoBehaviour
{
    [SerializeField]
    Well well;

    [SerializeField]
    GameObject block;

    void Start()
    {
        block.SetActive(true);
        block.layer = LayerMask.NameToLayer("Ground");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && well.view)
        {
            block.layer = LayerMask.NameToLayer("Platform");
            block.SetActive(false);

            well.HidePlayerLayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && well.view)
        {
            block.layer = LayerMask.NameToLayer("Ground");
            block.SetActive(true);
            well.view = false;
            well.ResetPlayerlayer();
        }
    }
}
