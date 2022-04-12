using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIHolder : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
    Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    public Image timeBar;
    public TextMeshPro feedbackText, comboText, scoreText, scoreText2P;
    public TextMeshProUGUI resultScore, resultCombo, rank,result1P,result2P, result1P2, result2P2,ID,tutrial;
    public int score,score2P,combo, combo2P, maxcombo=0, maxcombo2P = 0, gameLevel,gameLevel2p,turn,level_limit=12,is_hardmode=0;
    float time=0,realtime;
    public bool is_gamestart,is_vsmode,is_tutrial,is_smallaction;
    public Animation homeruAnime, comboAnime, scoreAnime, scoreAnime2P, resultAnime,resultanimevs,tutrialanime;
    public AudioSource sco;
    public ParticleSystem home;
    public GameObject kabe,hari,mizhari,feedbackbox,tubox;
    public static int playerID=0;
    public ClockEditor editor;

    public float timelimit=90;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        realtime = Time.realtimeSinceStartup;
        is_tutrial = true;
        if (is_vsmode)
        {
            kabe.transform.position = new Vector3(0, 0, 5);
            hari.layer = 10;
            mizhari.layer = 11;
            feedbackText.fontSize = 12;
            feedbackText.gameObject.transform.position = new Vector3(0,0,-2f);
            feedbackText.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        tutrial.text = "両手を横に開いてください";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath+"/shashin.png");
            Debug.Log("とった");
        }
        if (Input.GetButtonDown("NormalQuest"))
            is_hardmode = 0;
        if (Input.GetButtonDown("CalculateQuest1"))
            is_hardmode = 1;
        if (Input.GetButtonDown("CalculateQuest2"))
            is_hardmode = 2;


        if (Input.GetButtonDown("LimitLevel3"))
            level_limit = 3;
        if (Input.GetButtonDown("LimitLevel6"))
            level_limit = 6;
        if (Input.GetButtonDown("LimitLevel9"))
            level_limit = 9;
        if (Input.GetButtonDown("LimitLevel12"))
            level_limit = 12;

        if (is_gamestart)
        {
            if (!is_tutrial)
            {
                time = Time.realtimeSinceStartup - realtime;
                if (is_smallaction)
                {
                    tubox.SetActive(true);
                    tutrial.text = "大きく動かしてください";
                    if (!tutrialanime.isPlaying)
                        tutrialanime.Play("poseiroiro");
                }
                else
                    tubox.SetActive(false);
            }
            else
                realtime = Time.realtimeSinceStartup;
            if (Input.GetButtonDown("Retire"))
                time = timelimit+1;
            //時間切れのスコア表示
            if (time > timelimit&&turn==1)
            {
                is_gamestart = false;
                editor.LogEdit(false, 2);
                Time.timeScale = 1;
                //ここで対戦モードの結果発表と区別したい
                resultMessage();
                if (is_vsmode)
                    resultanimevs.Play();
                else
                    resultAnime.Play();
            }
            ID.text = "";
        }
        else
        {
            realtime = Time.realtimeSinceStartup;
            tubox.SetActive(false);
            if (time == 0 && Input.GetButtonDown("StartLevel1"))
            {
                gameLevel = 1;
                gameLevel2p = 1;
                is_gamestart = true;
                is_tutrial = true;
                tubox.SetActive(true);
                tutrialanime.Play("maenarae");
            }
            if (time == 0 && Input.GetButtonDown("StartLevel4"))
            {
                gameLevel = 4;
                gameLevel2p = 4;
                is_gamestart = true;
                is_tutrial = true;
                tubox.SetActive(true);
                tutrialanime.Play("maenarae");
            }
            if (time == 0 && Input.GetButtonDown("StartLevel7"))
            {
                gameLevel = 7;
                gameLevel2p = 7;
                is_gamestart = true;
                is_tutrial = true;
                tubox.SetActive(true);
                tutrialanime.Play("maenarae");
            }
            if (time == 0 && Input.GetButtonDown("StartLevel10"))
            {
                gameLevel = 10;
                gameLevel2p = 10;
                is_gamestart = true;
                is_tutrial = true;
                tubox.SetActive(true);
                tutrialanime.Play("maenarae");
            }
            if (Input.GetButtonDown("Retry"))
            {
                playerID++;
                SceneManager.LoadScene("TestScene");
            }
            ID.text = playerID.ToString("D2");
        }

        if (is_vsmode)
        {
            if (turn == 1)
            {
                home.gameObject.layer = 10;
                feedbackText.gameObject.layer = 10;
            }
            else
            {
                home.gameObject.layer = 11;
                feedbackText.gameObject.layer = 11;
            }
            scoreText2P.text = "Score " + score2P;
        }
        scoreText.text = "Score " + score;

        timeBar.fillAmount = (timelimit-time)/ timelimit;
        if (time > timelimit/2)
            timeBar.color = Color.yellow;
        if (time > timelimit*9/10)
            timeBar.color = Color.red;

        comboText.text = "<size=12>" + combo + "</size>\ncombo!";
    }

    public void Homeru(int combo)
    {
        switch (combo)
        {
            case 1:
                feedbackText.text = "Good!";
                break;
            case 2:
                feedbackText.text = "Great!";
                break;
            case 3:
                feedbackText.text = "VeryGood!";
                break;
            case 4:
                feedbackText.text = "Wonderful!";
                break;
            case 5:
                feedbackText.text = "Awesome!";
                break;
            default:
                feedbackText.text = "Fantastic!";
                break;
        }
        homeruAnime.Play();
        home.Play();
    }

    public void ComboAdd()
    {
        if (turn == 1)
        {
            combo++;
            comboAnime.Play();
            if (maxcombo < combo)
                maxcombo = combo;
            Homeru(combo);
            ScoreAdd(1000 + 200 * combo);
        }
        else
        {
            combo2P++;
            comboAnime.Play();
            if (maxcombo2P < combo2P)
                maxcombo2P = combo2P;
            Homeru(combo2P);
            ScoreAdd(1000 + 200 * combo2P);
        }
    }

    public void ScoreAdd(int addScore)
    {
        if (turn==1)
        {
            score += addScore;
            if(!is_vsmode)
                scoreAnime.Play("VScore");
            else
                scoreAnime.Play("VScore");
            sco.Play();
        }
        else
        {
            score2P += addScore;
            scoreAnime2P.Play();
            sco.Play();
        }
    }

    void resultMessage()
    {
        if (is_vsmode)
        {
            if (score > score2P)
            {
                result1P.text = "Win!!";
                result1P2.text = "Win!!";
                result2P.text = "Lose...";
                result2P2.text = "Lose...";
                result1P.color = Color.red;
                result2P.color = Color.blue;
            }
            else if (score< score2P)
            {
                result1P.text = "Lose...";
                result1P2.text = "Lose...";
                result2P.text = "Win!!";
                result2P2.text = "Win!!";
                result2P.color = Color.red;
                result1P.color = Color.blue;
            }
            else
            {
                result1P.text = "Draw";
                result1P2.text = "Draw";
                result2P.text = "Draw";
                result2P2.text = "Draw";
                result1P.color = Color.green;
                result2P.color = Color.green;
            }
        }
        else
        {
            resultScore.text = "Score <size=86>" + score + "</size>";
            resultCombo.text = "Max Combo <size=86>" + maxcombo + "</size>";
            if (score < 5000)
                rank.text = "D";
            else if (score < 10000*(timelimit/90))
                rank.text = "C";
            else if (score < 15000 * (timelimit / 90))
                rank.text = "B";
            else if (score < 20000 * (timelimit / 90))
                rank.text = "A";
            else if (score < 30000 * (timelimit / 90))
                rank.text = "S";
            else if (score < 40000 * (timelimit / 90))
                rank.text = "SS";
            else if (score < 50000 * (timelimit / 90))
                rank.text = "SSS";
            else
                rank.text = "GOD";
        }
    }
}
