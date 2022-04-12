using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doredaTitle : MonoBehaviour
{
    float quest_number;
    [SerializeField]
    Text quest_text;
    [SerializeField]
    Image pizza;
    float preview_time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        preview_time += Time.deltaTime;

        if (preview_time < 1)
        {
            quest_text.color = new Color(0, 0, 0, preview_time);
            pizza.color = new Color(1, 1, 1, preview_time);
        }else if (preview_time < 4)
        {
            quest_text.color= new Color(0, 0, 0, 1);
            pizza.color = new Color(1, 1, 1, 1);
        }
        else if(preview_time<5)
        {
            quest_text.color = new Color(0, 0, 0, 5-preview_time);
            pizza.color = new Color(1, 1, 1, 5-preview_time);
        }
        else
        {
            quest_text.color = new Color(0, 0, 0, 0);
            pizza.color = new Color(1, 1, 1, 0);
            QuestChange();
            preview_time = 0;
        }
    }

    void QuestChange()
    {

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
        pizza.fillAmount = quest_number;
    }
}
