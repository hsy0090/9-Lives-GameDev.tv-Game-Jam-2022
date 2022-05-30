using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoControl : MonoBehaviour
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
    public List<GameObject> Magazine = new List<GameObject>();
    public List<GameObject> MagazineSlot = new List<GameObject>();
    public int currentSlot = 0;
    public bool old = false;
    public int ammocount = 0;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------


    [SerializeField]
    Sprite[] img;
    // Start is called before the first frame update
    void Start()
    {
        if (old)
        {
            while (Magazine.Count < 6)
            {
                Magazine.Add(null);
            }
            for (int i = 0; i < Magazine.Count; i++)
            {
                if (Magazine[i] == null)
                {
                    MagazineSlot[i].GetComponent<Image>().enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < Magazine.Count; i++)
            {
                if (Magazine[i] != null)
                {
                    ammocount++;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                MagazineSlot[i].GetComponent<Image>().enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(old)
        {
            if (Magazine.Count > 6)
            {
                Magazine.RemoveRange(6, Magazine.Count - 6);
            }

        }
        if (currentSlot > Magazine.Count - 1)
        {
            currentSlot = 0;
        }
        if (!old && ammocount <= img.Length && gameObject.GetComponent<Image>().sprite != img[ammocount])
        {
            gameObject.GetComponent<Image>().sprite = img[ammocount];
        }
    }
}
