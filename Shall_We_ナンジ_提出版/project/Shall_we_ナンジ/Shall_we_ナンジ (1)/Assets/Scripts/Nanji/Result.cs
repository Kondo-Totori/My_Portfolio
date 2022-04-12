using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour {

    int s,c,o,r, e;
    public int score, is_s, is_c, is_o, is_r, is_e;
    public bool is_score, is_rank,is_ranked;
    public TextMeshProUGUI[] ScoreAndRank=new TextMeshProUGUI[4];
    public AudioSource judgeSound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (is_score)
            ScoreAndRank[0].text = "スコア";
        if (is_s == 0)
            ScoreAndRank[1].text = "";
        else if (is_s == 1)
            ScoreAndRank[1].text = Random.Range(0, 10).ToString();
        else if (is_s == 2)
        {
            //if(s!=0)
                ScoreAndRank[1].text = s.ToString();
            //else
              //  ScoreAndRank[1].text = "  ";
        }

        if (is_c == 0)
            ScoreAndRank[1].text += "";
        else if (is_c == 1)
            ScoreAndRank[1].text += Random.Range(0, 10).ToString();
        else if (is_c == 2)
        {
            //if (s!=0||c != 0)
                ScoreAndRank[1].text += c;
           // else
             //   ScoreAndRank[1].text += "  ";
        }

        if (is_o == 0)
            ScoreAndRank[1].text += "";
        else if (is_o == 1)
            ScoreAndRank[1].text += Random.Range(0, 10).ToString();
        else if (is_o == 2)
        {
           // if (s != 0 || c != 0||o != 0)
                ScoreAndRank[1].text += o;
           // else
            //    ScoreAndRank[1].text += "  ";
        }

        if (is_r == 0)
            ScoreAndRank[1].text += "";
        else if (is_r == 1)
            ScoreAndRank[1].text += Random.Range(0, 10).ToString();
        else if (is_r == 2)
        {
          //  if (s != 0 || c != 0 || o != 0||r != 0)
                ScoreAndRank[1].text += r;
          //  else
            //    ScoreAndRank[1].text += "  ";
        }

        if (is_e == 0)
            ScoreAndRank[1].text += "";
        else if (is_e== 1)
            ScoreAndRank[1].text += Random.Range(0, 10).ToString();
        else if (is_e == 2)
                ScoreAndRank[1].text += e;
        if (is_rank)
            ScoreAndRank[2].text = "ランク";
        if(is_ranked)
            ScoreAndRank[3].text = judge(score);
    }

    public string judge(int score)
    {
        if (score < 1000)
            return "D";
        else if (score >= 1000 && score < 3000)
            return "C";
        else if (score >= 3000 && score < 5000)
            return "B";
        else if (score >= 5000 && score < 7000)
            return "A";
        else if (score >= 7000 && score < 9000)
            return "S";
        else if (score >= 9000 && score < 12000)
            return "SS";
        else if (score >= 12000)
            return "SSS";
        else
            return "D";
    }

    public void endanime()
    {
        e = score % 10;
        r = score / 10 % 10;
        o = score / 100 % 10;
        c = score / 1000 % 10;
        s = score / 10000 % 10;
        GetComponent<Animation>().Play();
    }

    public void PlayJudge()
    {
        judgeSound.Play();
    }
}
