using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    private static God instance;

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
    }

    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.H))
            SetText("OOF");

        timer -= Time.deltaTime;

        if(timer <= 0.0f && !displaying)
        {
            SetText(randomlines[Random.Range(0, randomlines.Length)]);
            displaying = true;
        }
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
}
