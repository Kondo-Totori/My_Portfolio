using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public int gameLevel;
    int is_start=0;
    float starttimer;
    QuestionGenerator gen;
    public Image gauge;
    public GameObject[] sections;
    public FirstRound fst;
    public Animation tutrial;
    public GameObject rawimage;
    // Use this for initialization
    void Start () {
        gen = GameObject.Find("Generator(Canvas)").GetComponent<QuestionGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
        sections[is_start].SetActive(true);
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameLevel = 0;
            is_start = 1;
            sections[0].SetActive(false);
            tutrial.Play("maenarae");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameLevel = 1;
            is_start = 1;
            sections[0].SetActive(false);
            tutrial.Play("maenarae");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameLevel = 2;
            is_start = 1;
            sections[0].SetActive(false);
            tutrial.Play("maenarae");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameLevel = 3;
            is_start = 1;
            sections[0].SetActive(false);
            tutrial.Play("maenarae");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            is_start = 0;
            starttimer = 0;
            sections[0].SetActive(true);
            sections[1].SetActive(false);
            tutrial.Play("maenarae");
        }

        if (is_start==1&&fst.ji==9&&fst.hun==15)
        {
            starttimer += Time.deltaTime;
            tutrial.Stop();
            rawimage.SetActive(false);
        }
        else if (starttimer > 0)
        {
            starttimer -= 0.02f;
            rawimage.SetActive(true);
            tutrial.Play("maenarae");
        }
        gauge.fillAmount = starttimer / 3;

        if (starttimer > 3)
        {
            sections[1].SetActive(false);
            is_start = 0;
            starttimer = 0;
            gen.level = gameLevel;
            gen.Refresh();
            gen.is_gamestart = true;
            gauge.fillAmount = 0;
            gameObject.SetActive(false);
        }
    }
}
