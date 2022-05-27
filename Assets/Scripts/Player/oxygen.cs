using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygen : MonoBehaviour
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
    public int numOfOxygens;
    public bool underwater = false;

    public List<GameObject> Oxygenslist = new List<GameObject>();
    public GameObject OxygenPrefab;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    /*[SerializeField]
    float OxygenGap = 5;*/

    [SerializeField]
    GameObject OxygenBar;

    [SerializeField]
    float Timer;

    [SerializeField]
    float Timerinit = 5;
    // Start is called before the first frame update
    void Start()
    {
        Timer = Timerinit;
        
        while (numOfOxygens > Oxygenslist.Count)
        {
            Vector3 OxygenPos = new Vector3(OxygenBar.transform.position.x + (OxygenPrefab.GetComponent<Renderer>().bounds.size.x) * Oxygenslist.Count,
                OxygenBar.transform.position.y, OxygenBar.transform.position.z);
            GameObject temp = Instantiate(OxygenPrefab, OxygenPos, Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<SpriteRenderer>().enabled = false;
            Oxygenslist.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (underwater)
        {
            if (Timer <= 0)
            {
                FindObjectOfType<Lives>().Death("Drown");
            }
            else
            {
                Timer -= Time.deltaTime;
            }
            for(int i = 0; i < Oxygenslist.Count; i++)
            {
                if(i * (Timerinit / numOfOxygens) >= Timer)
                {
                    Oxygenslist[i].GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    Oxygenslist[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < Oxygenslist.Count; i++)
            {
                    Oxygenslist[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            underwater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            underwater = false;
            Timer = Timerinit;
            
        }
    }
}
