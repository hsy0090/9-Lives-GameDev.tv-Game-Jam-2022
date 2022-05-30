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
    bool old = false;

    [SerializeField]
    GameObject DeathText;

    [SerializeField]
    Sprite[] img;

    // Start is called before the first frame update
    void Start()
    {
        life = PlayerSave.Instance.life;
        deathTag = PlayerSave.Instance.deathTag;
        if (PlayerSave.Instance.Teleport1)
        {
            Player.GetComponent<PlayerController>().Teleport1.GetComponent<Teleporter>().ActivatePortal();
        }
        if (PlayerSave.Instance.Teleport2)
        {
            Player.GetComponent<PlayerController>().Teleport2.GetComponent<Teleporter>().ActivatePortal();
        }
        if (PlayerSave.Instance.Teleport3)
        {
            Player.GetComponent<PlayerController>().Teleport3.GetComponent<Teleporter>().ActivatePortal();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (life > numOfLives)
        {
            life = numOfLives;
        }
        if (!old && life <= img.Length && gameObject.GetComponent<Image>().sprite != img[life])
        {
            gameObject.GetComponent<Image>().sprite = img[life];
        }
        if (old)
        {

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
            SavePlayer();
        }
    }
    public void SavePlayer()
    {
        PlayerSave.Instance.life = life;
        PlayerSave.Instance.deathTag = deathTag;
        PlayerSave.Instance.Teleport1 = Player.GetComponent<PlayerController>().Teleport1.GetComponent<Teleporter>().GetStatus();
        PlayerSave.Instance.Teleport2 = Player.GetComponent<PlayerController>().Teleport2.GetComponent<Teleporter>().GetStatus();
        PlayerSave.Instance.Teleport3 = Player.GetComponent<PlayerController>().Teleport3.GetComponent<Teleporter>().GetStatus();
    }
}
