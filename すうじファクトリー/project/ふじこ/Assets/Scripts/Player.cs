using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    Text player_text;
    float playerNumber;
    int passNumber=0;
    bool modechange;
    private arithmeticPanel[] info= new arithmeticPanel[4];
    private Terget goal;
    private Animation[] point= new Animation[4];
    public float speed = 5f;
    private main m;

    // Start is called before the first frame update
    void Start()
    {
        SetMode();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (modechange)
        {
            if (passNumber < 2)
                transform.localPosition = new Vector2(transform.localPosition.x + speed, transform.localPosition.y);
            else if (passNumber < 3)
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed);
            else if (passNumber < 5)
                transform.localPosition = new Vector2(transform.localPosition.x - speed, transform.localPosition.y);
        }

        switch (passNumber)
        {
            case 0:
                if (transform.localPosition.x > 0)
                {
                    GetPanelData(passNumber);
                    passNumber++;
                }
                break;
            case 1:
                if (transform.localPosition.x > 300)
                {
                    GetPanelData(passNumber);
                    passNumber++;
                }
                break;
            case 2:
                if (transform.localPosition.y < -100)
                {
                    GetPanelData(passNumber);
                    passNumber++;
                }
                break;
            case 3:
                if (transform.localPosition.x < 0)
                {
                    GetPanelData(passNumber);
                    passNumber++;
                }
                break;
            case 4:
                if (transform.localPosition.x < -300)
                {
                    GetResultInfo();
                    passNumber++;
                    Destroy(gameObject);
                }
                break;
        }
    }

    void GetPanelData(int panelNum)
    {
        playerNumber=info[panelNum].aritmetic(playerNumber);
        if (info[panelNum].GetArithNumber() != 0)
        {
            m.AddScore(100+m.ReturnCombo()*20);
            point[panelNum].gameObject.GetComponentInChildren<Text>().text = "+"+(100 + m.ReturnCombo() * 20);
            point[panelNum].Play("point");
            BGMManager.f_point = true;
        }
        player_text.text = playerNumber.ToString();
        info[panelNum].SetNull();
    }

    void GetObjInfo()
    {
        info[0] = GameObject.Find("Red").GetComponent<arithmeticPanel>();
        info[1] = GameObject.Find("Red (1)").GetComponent<arithmeticPanel>();
        info[2] = GameObject.Find("Red (2)").GetComponent<arithmeticPanel>();
        info[3] = GameObject.Find("Red (3)").GetComponent<arithmeticPanel>();
        for (int i = 0; i < 4; i++)
            point[i] = GameObject.Find("point_" + i).GetComponent<Animation>();
        m = GameObject.Find("Canvas").GetComponent<main>();
    }

    void GetResultInfo()
    {
        goal = GameObject.Find("2P").GetComponent<Terget>();
        goal.match(playerNumber);
    }

    public void SetMode()
    {
        GetObjInfo();
        playerNumber = Random.Range(5, 16);
        player_text.text = playerNumber.ToString();
        modechange = true;
        BGMManager.d_result = true;
    }
}
