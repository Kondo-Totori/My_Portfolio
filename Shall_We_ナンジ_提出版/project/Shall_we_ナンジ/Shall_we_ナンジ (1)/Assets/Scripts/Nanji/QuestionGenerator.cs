using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionGenerator : MonoBehaviour {

    int score,section,chain;
    public Text sectionText,seikaiText,correctText;
    public TextMeshProUGUI[] questText;
    public TextMeshProUGUI scoreText, countText;
    public bool is_gamestart,idol,is_basemode;
    public int level,correctji, correcthun;
    float questtime,checktime,para=0,answer_keep;
    bool resulted,is_ato,is_nowtime;
    int questji, questhun, baseji, basehun;
    public Image gauge;
    public GameObject nowtimeSet,startPanel;
    Round round;
    AnswerRound answerRound, answerRound2;
    BackChanger backChanger;
    Result result;
    public Image[] circles;
    public int vive;
    public Material correctParticle;
    bool no_correct=true;
    public Animation toideru,tutrial;
    public GameObject Hintset;

	// Use this for initialization
	void Start () {
        round = GameObject.Find("point").GetComponent<Round>();
        answerRound = GameObject.Find("Answerpoint").GetComponent<AnswerRound>();
        answerRound2 = GameObject.Find("Basepoint").GetComponent<AnswerRound>();
        backChanger = GameObject.Find("Main Camera").GetComponent<BackChanger>();
        result = GameObject.Find("ResultPanel").GetComponent<Result>();
        answerRound.gameObject.SetActive(false);
        answerRound2.gameObject.SetActive(false);
        result.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!is_nowtime && Input.GetKeyDown(KeyCode.N))
        {
            is_nowtime = true;
            nowtimeSet.SetActive(true);
        }
        else if (is_nowtime && Input.GetKeyDown(KeyCode.N))
        {
            is_nowtime = false;
            nowtimeSet.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        if (questtime <= 20&&!resulted)
            gauge.fillAmount = questtime / 20;
        else
            gauge.fillAmount = 0;
        if (is_gamestart)
        {
            questtime += Time.deltaTime;
            scoreText.text = "スコア：" + score;
            sectionText.text = "問題：" + section + "/5";
            if (idol)
            {
                Hintset.SetActive(true);
                tutrial.Play("poseiroiro");
            }
            else
                Hintset.SetActive(false);
            if (questtime >= 10 && questtime < 20)
                countText.text = (int)(21 - questtime) + "";
            else
                countText.text = "";
            if (round.ji == correctji && round.hun == correcthun)
                answer_keep += Time.deltaTime;
            else
                answer_keep = 0;
            if ((questtime > 20||answer_keep>3) && !resulted)
            {
                resulted = true;
                AnswerCheck();
            }
            if (resulted)
                checktime += Time.deltaTime;
            if (checktime > 2)
                Refresh();
        }

        if (para>0)
        {
            para -= 0.02f;
        }

        correctParticle.SetColor("_TintColor", new Color(1f, 1f, 1f, para));
    }

    public void AnswerCheck()
    {
        answerRound.is_set2 = false;
        answer_keep = 0;
        answerRound2.gameObject.SetActive(false);
        if (round.ji == correctji && round.hun == correcthun)
        {
            para = 1;
            answerRound.gameObject.SetActive(false);
            circles[section-1].color = Color.red;
            AudioManager.correctSE = true;
            vive = 1;
            score += 1600+chain*200;
            chain++;
            score += (int)((20 - questtime) * 100);
            correctText.text = correctji + "<size=54>時</size>" + correcthun + "<size=54>分</size>";
            backChanger.correct_anime = true;
            no_correct = false;
        }
        else
        {
            circles[section-1].color = Color.blue;
            AudioManager.incorrectSE = true;
            vive = 2;
            score += 100;
            chain = 0;
            if (correctji == round.ji)
                score += 500;
            if (correcthun == round.hun)
                score += 500;
            if (correctji == round.ji + 1 || correctji == round.ji - 1 || (correctji == 12 && round.ji == 1) || (correctji == 1 && round.ji == 12))
                score += 250;
            if (correcthun == round.hun + 1 || correcthun == round.hun - 1 || (correcthun == 12 && round.hun == 1) || (correcthun == 1 && round.hun == 12))
                score += 250;
            seikaiText.text = "正解は…";
            answerRound.gameObject.SetActive(true);
            answerRound.is_set = true;
            answerRound.SetAnswer(correctji,correcthun);
            correctText.text = correctji + "時" + correcthun + "分";
            backChanger.incorrect_anime = true;
        }
        questtime = 0;
    }

    public void Refresh()
    {
        checktime = 0;
        section++;
        AudioManager.questSE=true;
        if (section > 5)
        {
            is_gamestart = false;
            result.gameObject.SetActive(true);
            result.score = score;
            result.endanime();
            return;
        }
        is_ato = false;
        answerRound.gameObject.SetActive(false);
        backChanger.correct_anime = false;
        backChanger.incorrect_anime = false;
        questtime = 0;
        resulted = false;
        seikaiText.text = "";
        correctText.text = "";
        if (Random.Range(0, 2) == 1)
            is_ato = true;
        if (level >= 2)
            questji = Random.Range(1, 13);
        else
            questji = 0;
        if (level == 1 || level == 3)
            questhun = Random.Range(0, 12) * 5;
        else
            questhun = 0;
        baseji = Random.Range(1, 13);
        basehun = Random.Range(0, 12)*5;
        if (is_basemode)
            questText[0].text = "<color=orange><size=120>" + baseji + "</size>時<size=120>" + basehun + "</size>分</color><color=black>";
        else
            questText[0].text = "<color=black><size=120>"+baseji + "</size>時<size=120>" + basehun + "</size>分";
        if (questji != 0 || questhun != 0) {
            questText[0].text += "の\n";
            if (questji!=0)
               questText[0].text +=  "<size=120>"+ questji + "</size>時間";
            if (questhun != 0)
               questText[0].text += "<size=120>" + questhun + "</size>分";
            if (questji != 0 && questhun != 0)
                questText[0].text += "\n";
                if (is_ato)
                    questText[0].text += "<size=120>後</size>";
                else
                    questText[0].text += "<size=120>前</size>";
        }
        questText[0].text += "は？</color>";
        questText[1].text = questText[0].text;
        toideru.Play();
        if (is_ato)
        {
            correctji = baseji + questji;
            correcthun = basehun + questhun;
        }
        else
        {
            correctji = baseji - questji;
            correcthun = basehun - questhun;
        }
        while (correcthun < 0)
        {
            correctji -= 1;
            correcthun += 60;
        }
        while (correcthun >= 60)
        {
            correctji += 1;
            correcthun -= 60;
        }
        while (correctji < 1)
            correctji += 12;
        while (correctji >= 13)
            correctji -= 12;

        if (no_correct&&!is_basemode)
        {
            answerRound.gameObject.SetActive(true);
            answerRound.SetAnswer(correctji, correcthun);
            answerRound.is_set2 = true;
        }
        if (is_basemode)
        {
            answerRound2.gameObject.SetActive(true);
            answerRound2.SetAnswer(baseji, basehun);
            answerRound2.is_set2 = true;
        }
    }

    public void ResetGame()
    {
        for (int i = 0; i < 5; i++) {
            circles[i].color = Color.black;
        }
        score = 0;
        chain = 0;
        questText[0].text= "";
        section = 0;
        questtime = 0;
        is_gamestart = false;
        result.gameObject.SetActive(false);
        answerRound.gameObject.SetActive(false);
        backChanger.incorrect_anime = false;
        backChanger.correct_anime = false;
        startPanel.SetActive(true);
        scoreText.text = "スコア：" + score;
        sectionText.text = "問題：" + section + "/5";
        result.ScoreAndRank[0].text = "";
        result.ScoreAndRank[1].text = "";
        result.ScoreAndRank[2].text = "";
        result.ScoreAndRank[3].text = "";
        no_correct = true;
    }
}
