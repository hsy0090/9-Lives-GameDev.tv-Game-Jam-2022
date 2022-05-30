using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
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
    public int health;
    public int numOfHearts;

    public List<GameObject> hearts = new List<GameObject>();
    public GameObject heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float HeartGap = 5;

    [SerializeField]
    GameObject Life;

    void Start()
    {
        health = numOfHearts;
    }

    void Update()
    {
        if( health > numOfHearts)
        {
            health = numOfHearts;
        }
        //heart generation and destruction
        while(numOfHearts > hearts.Count)
        {
            Vector3 heartPos = new Vector3(transform.position.x + (heartPrefab.GetComponent<RectTransform>().rect.width + HeartGap) * hearts.Count,
                transform.position.y, transform.position.z);
            GameObject temp = Instantiate(heartPrefab, heartPos, Quaternion.identity);
            temp.transform.SetParent(transform);
            hearts.Add(temp);
        }
        while(hearts.Count > numOfHearts)
        {
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }
        //hp check
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {

                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }

        }

    }
    public void dealDamage(int damage, string damagetype)
    {
        if (!Life.GetComponent<Lives>().deathTag.Contains(damagetype))
        {
                health -= damage;
                if (health <= 0)
                {
                    if (God.Instance && !FindObjectOfType<Lives>().deathTag.Contains(damagetype))
                    {
                        switch(damagetype)
                        {
                        case "Bullet":
                            God.Instance.SetText("Don't feed the fishes", true);
                            break;
                        case "Reflected Bullet":
                            God.Instance.SetText("Ah....the betrayal", true);
                            break;
                        case "Fire":
                            God.Instance.SetText("Too hot to handle I guess...", true);
                            break;
                        default:
                            God.Instance.SetText("...", true);
                            break;
                        }
                        
                    }

                Life.GetComponent<Lives>().Death(damagetype);
                }
        }
    }
}
