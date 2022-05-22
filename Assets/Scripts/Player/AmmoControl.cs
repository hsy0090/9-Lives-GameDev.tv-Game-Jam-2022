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

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        while(Magazine.Count < 6)
        {
            Magazine.Add(null);
        }
        for(int i = 0; i < Magazine.Count; i++)
        {
            if(Magazine[i] == null)
            {
                MagazineSlot[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Magazine.Count > 6)
        {
            Magazine.RemoveRange(6, Magazine.Count - 6);
        }
        if(currentSlot > 5)
        {
            currentSlot = 0;
        }
    }
}
