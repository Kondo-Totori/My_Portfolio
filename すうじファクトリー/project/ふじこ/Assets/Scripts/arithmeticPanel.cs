using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class arithmeticPanel : MonoBehaviour
{
    public int type;
    public int number;
    [SerializeField]
    bool is_red;
    [SerializeField]
    bool is_droppedPanel;
    [SerializeField]
    Text number_text;
    [SerializeField]
    Sprite[] colors;

    // Start is called before the first frame update
    void Start()
    {
        if (is_droppedPanel)
            SetNull();
        else
            SetRandomNumber();
    }

    public float aritmetic(float motonokazu)
    {
        float result=0;
        if (is_red)
        {
            if (type < 8)
                result = motonokazu + number;
            else
                result = motonokazu * number;
        }
        else
        {
            if (type < 8)
                result = motonokazu - number;
            else
                result = motonokazu / number;
        }
        return result;
    }

    void SetText()
    {
        if (number == 0)
            number_text.text = "0";
        else
        {
            if (is_red)
            {
                if (type < 8)
                    number_text.text = "+" + number;
                else
                    number_text.text = "×" + number;
            }
            else
            {
                if (type < 8)
                    number_text.text = "-" + number;
                else
                    number_text.text = "÷" + number;
            }
        }
    }

    public void SetPanel(int color ,int getType,int getNumber)
    {
        if (color == 1)
        {
            is_red = true;
            GetComponent<Image>().sprite = colors[0];
        }
        else
        {
            is_red = false;
            GetComponent<Image>().sprite = colors[1];
        }

        type = getType;
        number = getNumber;

        BGMManager.f_set=true;

        SetText();
    }

    public int GetPanelColor()
    {
        if (is_red)
            return 1;
        else
            return 0;
    }

    public int GetArithType()
    {
        return type;
    }

    public int GetArithNumber()
    {
        return number;
    }

    public void SetNull()
    {
        GetComponent<Image>().sprite = colors[2];
        type = 1;
        is_red = true;
        number = 0;
        SetText();
    }

    public void SetNowColor()
    {
        if (number == 0)
            GetComponent<Image>().sprite = colors[2];
        else
        {
            if(is_red)
                GetComponent<Image>().sprite = colors[0];
            else
                GetComponent<Image>().sprite = colors[1];
        }
    }

    public void SetRandomNumber()
    {
        if (Random.Range(0, 2) == 1)
        {
            is_red = true;
            GetComponent<Image>().sprite = colors[0];
        }
        else
        {
            is_red = false;
            GetComponent<Image>().sprite = colors[1];
        }

        type = Random.Range(0, 10);
        if (type < 8)
            number = Random.Range(1, 10);
        else
            number = 2;

        SetText();
    }

    public void ShuffleSound()
    {
        BGMManager.f_shuffle = true;
    }
}
