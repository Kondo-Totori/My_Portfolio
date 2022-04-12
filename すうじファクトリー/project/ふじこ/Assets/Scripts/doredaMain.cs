using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doredaMain : MonoBehaviour
{
    float quest_number;
    float[] dummy_number =  new float[3];
    int correct_number, score,count;
    float time;
    [SerializeField]
    Text quest_text,time_text,score_text;
    [SerializeField]
    Image[] pizza;
    bool is_gameStart;
    [SerializeField]
    Text resultScore, resultCount, resultComment;
    [SerializeField]
    Animation result;
    [SerializeField]
    GameObject resultPanel,TutrialPanel;
    private SceneChanger changer;

    // Start is called before the first frame update
    void Start()
    {
        time_text.text = "60";
        changer = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_gameStart)
        {
            time += Time.deltaTime;
            if (time > 60)
            {
                time = 0;
                is_gameStart = false;
                Result();
            }
            score_text.text = ":" + score;
            time_text.text = (Mathf.FloorToInt(61 - time)).ToString("D2");
        }

    }

    void QuestSelect()
    {
        int runner;
        if (Random.Range(0, 3) > 0)
        {
            switch (Random.Range(1, 21))
            {
                case 1:
                    quest_number = 0.5f;
                    quest_text.text = "1/2";
                    break;
                case 2:
                    quest_number = 0.33f;
                    quest_text.text = "1/3";
                    break;
                case 3:
                    quest_number = 0.25f;
                    quest_text.text = "1/4";
                    break;
                case 4:
                    quest_number = 0.2f;
                    quest_text.text = "1/5";
                    break;
                case 5:
                    quest_number = 0.17f;
                    quest_text.text = "1/6";
                    break;
                case 6:
                    quest_number = 0.14f;
                    quest_text.text = "1/7";
                    break;
                case 7:
                    quest_number = 0.125f;
                    quest_text.text = "1/8";
                    break;
                case 8:
                    quest_number = 0.667f;
                    quest_text.text = "2/3";
                    break;
                case 9:
                    quest_number = 0.5f;
                    quest_text.text = "2/4";
                    break;
                case 10:
                    quest_number = 0.75f;
                    quest_text.text = "3/4";
                    break;
                case 11:
                    quest_number = 0.4f;
                    quest_text.text = "2/5";
                    break;
                case 12:
                    quest_number = 0.6f;
                    quest_text.text = "3/5";
                    break;
                case 13:
                    quest_number = 0.8f;
                    quest_text.text = "4/5";
                    break;
                case 14:
                    quest_number = 0.33f;
                    quest_text.text = "2/6";
                    break;
                case 15:
                    quest_number = 0.5f;
                    quest_text.text = "3/6";
                    break;
                case 16:
                    quest_number = 0.667f;
                    quest_text.text = "4/6";
                    break;
                case 17:
                    quest_number = 0.25f;
                    quest_text.text = "2/8";
                    break;
                case 18:
                    quest_number = 0.375f;
                    quest_text.text = "3/8";
                    break;
                case 19:
                    quest_number = 0.5f;
                    quest_text.text = "4/8";
                    break;
                case 20:
                    quest_number = 0.75f;
                    quest_text.text = "6/8";
                    break;
            }

        }
        else
        {
            quest_number = Random.Range(1, 10) / 10f;
            quest_text.text = quest_number.ToString("F1");
        }
        correct_number = Random.Range(0, 4);
        pizza[correct_number].fillAmount = quest_number;
        runner = 0;
        dummy_number[0] = quest_number + Random.Range(0.1f, 0.4f);
        dummy_number[1] = quest_number + Random.Range(0.35f, 0.55f);
        dummy_number[2] = quest_number + Random.Range(0.6f, 0.9f);
        for (int i = 0; i < 3; i++)
        {
            if (dummy_number[i] > 1)
                dummy_number[i] -= 1;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i != correct_number)
            {
                pizza[i].fillAmount = dummy_number[runner];
                runner++;
            }
        }
    }

    public void PushPizza_1()
    {
        if (is_gameStart)
        {
            if (correct_number==0)
            {
                //1
                score += 100;
                count++;
                QuestSelect();
                BGMManager.d_correct = true;
            }
            else
            {
                //2
                score -= 50;
                BGMManager.d_incorrect = true;
            }

        }
        if (score < 0)
            score = 0;
    }

    public void PushPizza_2()
    {
        if (is_gameStart)
        {
            if (correct_number == 1)
            {
                //1
                score += 100;
                count++;
                QuestSelect();
                BGMManager.d_correct = true;
            }
            else
            {
                //2
                score -= 50;
                BGMManager.d_incorrect = true;
            }

        }
        if (score < 0)
            score = 0;
    }

    public void PushPizza_3()
    {
        if (is_gameStart)
        {
            if (correct_number == 2)
            {
                //1
                score += 100;
                count++;
                QuestSelect();
                BGMManager.d_correct = true;
            }
            else
            {
                //2
                score -= 50;
                BGMManager.d_incorrect = true;
            }

        }
        if (score < 0)
            score = 0;
    }

    public void PushPizza_4()
    {
        if (is_gameStart)
        {
            if (correct_number == 3)
            {
                //1
                score += 100;
                count++;
                QuestSelect();
                BGMManager.d_correct = true;
            }
            else
            {
                //2
                score -= 50;
                BGMManager.d_incorrect = true;
            }

        }
        if (score < 0)
            score = 0;
    }

    void Result()
    {
        SetResultText();
        resultPanel.SetActive(true);
        result.Play("doredaResult");
    }

    void SetResultText()
    {
        resultScore.text = score.ToString();
        resultCount.text = count.ToString();

        if (score < 500)
        {
            resultComment.text = "焦げたピザ級";
        }
        else if (score < 1000)
        {
            resultComment.text = "ピザ見習い級";
        }
        else if (score < 1500)
        {
            resultComment.text = "マリナーラ級";
        }
        else if (score < 2000)
        {
            resultComment.text = "ポルチーニ級";
        }
        else if (score < 2500)
        {
            resultComment.text = "ジェノベーゼ級";
        }
        else if (score < 3000)
        {
            resultComment.text = "マルゲリータ級";
        }
        else if (score < 3500)
        {
            resultComment.text = "ピザ職人級";
        }
        else
        {
            resultComment.text = "ピザ神級";
        }
    }

    public void GameStart()
    {
        TutrialPanel.SetActive(false);
        resultPanel.SetActive(false);
        if (!is_gameStart)
        {
            BGMManager.d_confirm = true;
            QuestSelect();
            count = 0;
            score = 0;
            is_gameStart = true;
        }
    }

    public void GoTitle()
    {
        BGMManager.d_confirm = true;
        changer.PizzaToTitle();
    }
}
