using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    private static God instance;

    [Header("Text Display")]
    [SerializeField]
    TMPro.TMP_Text godtext;
    [SerializeField]
    string[] randomlines;
    string curline;
    string display;
    [SerializeField]
    float txtdelay = 0.1f;
    [SerializeField]
    float randomtxttime = 10.0f;
    float timer;
    bool displaying;

    [Header("Object Control")]
    [SerializeField]
    GameObject timeicon;
    [SerializeField]
    TimeObj[] moveobjs;
    [SerializeField]
    float reversaltime = 2.5f;
    bool reversetime = false;
    float reversetimer;

    private God()
    {
    }

    public static God Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        instance = this;
        displaying = false;
        godtext.text = "";
        timer = randomtxttime;

        moveobjs = FindObjectsOfType<TimeObj>();
        reversetime = false;
        timeicon.SetActive(false);
    }

    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetText("OOF");
            ReverseGravity();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SetText("Eh...");
            timeicon.SetActive(true);
            reversetime = true;
        }

        timer -= Time.deltaTime;

        if(timer <= 0.0f && !displaying)
        {
            SetText(randomlines[Random.Range(0, randomlines.Length)]);
            displaying = true;
        }
    }

    private void FixedUpdate()
    {
        if (reversetime) {
            Rewind();
            reversetimer += Time.deltaTime;
        }
        else
            Record();
    }

    public void SetText(string _text)
    {
        curline = _text;
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        for(int i = 0; i < curline.Length; i++)
        {
            display = curline.Substring(0, i+1);
            godtext.text = display;
            yield return new WaitForSeconds(txtdelay);
        }

        yield return new WaitForSeconds(1.5f);
        godtext.text = "";
        displaying = false;
        timer = randomtxttime;
    }

    public void ReverseGravity()
    {
        foreach(TimeObj b in moveobjs) {
            b.GetComponent<Rigidbody2D>().gravityScale *= -1;
        }
    }

    void Record()
    {
        foreach (TimeObj b in moveobjs) {
            b.rewinding = false;
            b.reversepos.Insert(0, b.transform.position);
        }
    }

    public void Rewind()
    {
        foreach (TimeObj b in moveobjs) {

            b.rewinding = true; 
            if (b.reversepos.Count > 0)
            {
                b.transform.position = b.reversepos[0];
                b.reversepos.RemoveAt(0);
            }
        }

        if (reversetimer > reversaltime)
        {
            reversetime = false;
            reversetimer = 0.0f;
            timeicon.SetActive(false);
        }
    }
}
