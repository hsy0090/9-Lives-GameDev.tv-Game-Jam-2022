using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
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
    public int life;
    public int numOfLives = 9;
    public List<string> deathTag = new List<string>();
    public bool Teleport1;
    public bool Teleport2;
    public bool Teleport3;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------

    // Start is called before the first frame update
    public static PlayerSave Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            life = numOfLives;
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetsave()
    {
        life = numOfLives;
        deathTag.Clear();
        Teleport1 = false;
        Teleport2 = false;
        Teleport3 = false;
    }
}
