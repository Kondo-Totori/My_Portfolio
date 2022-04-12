using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject startPosition;
    [SerializeField]
    GameObject pObj;
    private float time;
    [SerializeField]
    float speed = 1f;
    public bool is_gamestart;
    [SerializeField]
    GameObject sPanel;
    [SerializeField]
    GameObject rPanel;
    private int Score;
    public int limit;
    [SerializeField]
    Text ScoreText,rScoreText;
    private int combo = 0;
    [SerializeField]
    Animation comboAnime;
    [SerializeField]
    Text comboText, rcomboText,judgeText;
    [SerializeField]
    Text limitText;
    [SerializeField]
    Animation resultAnime;
    private int correctCount;
    private SceneChanger changer;
    [SerializeField]
    Terget tar;

    // Start is called before the first frame update
    void Start()
    {
        changer = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(is_gamestart)
            time += Time.deltaTime;
        if (time > 15/speed&&limit>0)
        {
            time =0;
            EditPlayer();
        }
    }

    void EditPlayer()
    {
        --limit;
        GameObject p;
        p=Instantiate(player, startPosition.transform.position, Quaternion.identity);
        p.transform.parent = pObj.transform;
        p.GetComponent<Player>().speed = speed;
        limitText.text = "ノコリ : " + limit+" コ";
    }

    public void GameStart()
    {
        BGMManager.f_confirm = true;
        correctCount = 0;
        limit = 10;
        Score = 0;
        is_gamestart = true;
        sPanel.SetActive(false);
        rPanel.SetActive(false);
        tar.SetTarget();
        time = 12;
        ScoreText.text = "スコア : 0";
    }

    public void AddScore(int plusScore)
    {
        Score += plusScore;
        ScoreText.text = "スコア : "+Score;
    }

    public void AddCombo()
    {
        combo++;
        BGMManager.f_combo = true;
        comboText.text = combo + "\n<size=24>COMBO!!</size>";
        comboAnime.Play("chara");
        speed += 0.2f;
        correctCount++;
    }

    public void ResetCombo()
    {
        combo = 0;
        //speed = 1f;
    }

    public int ReturnCombo()
    {
        return combo;
    }

    public int ScoreCalculate()
    {
        int plusScore=800+combo*200;
        return plusScore;
    }

    public void ResultAnime()
    {
        is_gamestart = false;
        rPanel.SetActive(true);
        rScoreText.text = Score.ToString();
        rcomboText.text = correctCount.ToString();
        judgeText.text = Judge(Score);
        resultAnime.Play("Result");
    }

    string Judge(int score)
    {
        if (score < 2000)
            return "E";
        else if (score < 5000)
            return "D";
        else if (score < 8000)
            return "C";
        else if (score < 11000)
            return "B";
        else if (score < 14000)
            return "A";
        else if (score < 17000)
            return "S";
        else if (score < 20000)
            return "SS";
        else
            return "SSS";
    }

    public void GoTitle()
    {
        BGMManager.f_confirm = true;
        changer.FactoryToTitle();
    }
}
