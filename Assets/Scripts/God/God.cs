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
    [System.NonSerialized]
    public bool displaying;
    [SerializeField]
    GameObject potrait;
    [SerializeField]
    AudioSource talk;

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
        potrait.SetActive(false);
        godtext.text = "";
        talk.Stop();
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

    public void SetText(string _text, bool takeover = false)
    {
        if (displaying && !takeover)
            return;

        displaying = true;
        curline = _text;
        talk.Play();
        StartCoroutine(DisplayText());
    }

    public void SetTextButton(string _text)
    {
        displaying = true;
        curline = _text;
        talk.Play();
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        potrait.SetActive(true);
        for (int i = 0; i < curline.Length; i++)
        {
            display = curline.Substring(0, i+1);
            godtext.text = display;
            yield return new WaitForSecondsRealtime(txtdelay);
        }
        talk.Stop();
        yield return new WaitForSecondsRealtime(1.5f);
        godtext.text = "";
        displaying = false;
        timer = randomtxttime;
        potrait.SetActive(false);
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
            if (b != null)
            {
                b.rewinding = false;
                b.reversepos.Insert(0, b.transform.position);
            }
        }
    }

    public void Rewind()
    {
        foreach (TimeObj b in moveobjs) {

            if (!b)
                continue;

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

    public void ReverseTime()
    {
        timeicon.SetActive(true);
        reversetime = true;
        SetText("Ooh I don't think so");
    }

    public void RandomLines()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f && !displaying)
        {
            SetText(randomlines[Random.Range(0, randomlines.Length)]);
            ResetRandLines();
        }
    }

    public void ResetRandLines()
    {
        timer = randomtxttime;
    }
}
