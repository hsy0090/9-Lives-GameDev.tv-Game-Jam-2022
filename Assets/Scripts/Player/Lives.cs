using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lives : MonoBehaviour
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
    public int numOfLives;

    public List<GameObject> lives = new List<GameObject>();
    public GameObject LifePrefab;
    public GameObject Player;
    public GameObject health;
    public GameObject DeathPanel;
    public List<string> deathTag = new List<string>();
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float LivesGap = 5;

    

    [SerializeField]
    GameObject DeathText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life > numOfLives)
        {
            life = numOfLives;
        }
        //heart generation and destruction
        while (numOfLives > lives.Count)
        {
            Vector3 heartPos = new Vector3(transform.position.x + (LifePrefab.GetComponent<RectTransform>().rect.width + LivesGap) * lives.Count,
                transform.position.y, transform.position.z);
            GameObject temp = Instantiate(LifePrefab, heartPos, Quaternion.identity);
            temp.transform.SetParent(transform);
            lives.Add(temp);
        }
        while (lives.Count > life)
        {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
    }
    public void Death(string deathtype)
    {
        if (!deathTag.Contains(deathtype))
        {
            deathTag.Add(deathtype);
            life -= 1;
            DeathPanel.SetActive(true);
            DeathText.GetComponent<TMPro.TextMeshProUGUI>().text = ("Death By: " + deathtype);
            FindObjectOfType<GameManager>().Pause();
        }
    }
}
