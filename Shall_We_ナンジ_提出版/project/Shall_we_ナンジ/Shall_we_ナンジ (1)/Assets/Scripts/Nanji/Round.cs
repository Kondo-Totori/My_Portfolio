using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Round : MonoBehaviour {

    public GameObject[] hari;
    public int ji,hun;
    public float rotji, rothun,idoltime,smallacttime;
    public TextMeshProUGUI nowtime;
    float pitch=1;
    public QuestionGenerator gen;
    public GameObject ClockSE;
    public GameObject sun, moon;
    Cursor[] cursors = new Cursor[2];
    public bool is_vsmode,is_jiauto, is_hunauto;

    // Use this for initialization
    void Start () {
        if(SceneManager.GetActiveScene().name=="SampleScene")
            gen = GameObject.Find("Generator(Canvas)").GetComponent<QuestionGenerator>();
        cursors[0] = GameObject.Find("player").GetComponent<Cursor>();
        cursors[1] = GameObject.Find("player (1)").GetComponent<Cursor>();
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (gen.is_gamestart)
                idoltime += Time.deltaTime;
            if (idoltime > 5)
                gen.idol = true;
            else
                gen.idol = false;
        }
        if (Input.GetButtonDown("AutoRed"))
        {
            if (!is_hunauto)
                is_hunauto = true;
            else
                is_hunauto = false;
        }
        if (Input.GetButtonDown("AutoBlue"))
        {
            if (!is_jiauto)
                is_jiauto = true;
            else
                is_jiauto = false;
        }

        //針がマイナスの値にならないようにする

        rothun = -cursors[1].rad * Mathf.Rad2Deg + 90;
        rotji = -cursors[0].rad * Mathf.Rad2Deg + 90;

        if (rotji <= 0)
        {
            rotji += 360;
        }
        if (rotji > 360)
        {
            rotji -= 360;
        }
        if (rothun <= 0)
        {
            rothun += 360;
        }
        if (rothun > 360)
        {
            rothun -= 360;
        }

        if(!is_jiauto)
            if (cursors[0].kyori > 7)
                hari[0].transform.rotation = Quaternion.Euler(0, 0, -rotji);
            else
                if(rotji>=90&&rotji<=270)
                    hari[0].transform.rotation = Quaternion.Euler(-90 + (90 / 7) * cursors[0].kyori, (-90 + (90 / 7) * cursors[0].kyori)/-rotji, -rotji);
                else
                    hari[0].transform.rotation = Quaternion.Euler(90 - (90 / 7) * cursors[0].kyori, (90 - (90 / 7) * cursors[0].kyori)/-rotji, -rotji);
        else
            hari[0].transform.rotation = Quaternion.Euler(0, 0, -ji * 30 - hun / 2);
        if (!is_hunauto)
            if (cursors[1].kyori > 7)
                hari[1].transform.rotation = Quaternion.Euler(0, 0, -rothun);
            else
                if(rothun>=90&&rothun<=270)
                    hari[1].transform.rotation = Quaternion.Euler(-90 + (90 / 7) * cursors[1].kyori, (90 + (90 / 7) * cursors[1].kyori)/-rothun, -rothun);
                else
                    hari[1].transform.rotation = Quaternion.Euler(90 - (90 / 7) * cursors[1].kyori, (90 - (90 / 7) * cursors[1].kyori)/-rothun, -rothun);
        else
                hari[1].transform.rotation = Quaternion.Euler(0, 0, -hun * 6);

        if ((!is_jiauto && cursors[0].is_smallact) || (!is_hunauto && cursors[1].is_smallact))
            smallacttime += Time.deltaTime;
        else
            smallacttime = 0;

        //時と分の取得
        //hun = Returnhun(rothun);
        //ji = Returnji(rotji,hun);
        //nowtime.text = ji + "<size=54>時</size>" + hun + "<size=54>分</size>";

        //sun.transform.localPosition = new Vector3(-640*Mathf.Cos((60*(ji-6)+hun)*pitch ), 320 * Mathf.Sin((60 * (ji - 6) + hun) * pitch / 180));
        //moon.transform.localPosition = new Vector3(-sun.transform.localPosition.x, -sun.transform.localPosition.y);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (pitch > 1)
                pitch -= 0.02f;
            else if (pitch < 1)
                pitch += 0.02f;
            if (pitch > 2.5f)
                pitch = 2.5f;
            if (pitch < -1.5f)
                pitch = -1.5f;

            if ((pitch >= 0.98f && pitch <= 1.02f) || !gen.is_gamestart)
                pitch = 1;

            AudioManager.BGMpitch = pitch;
        }
    }

    public int Returnji(float rotationz,int hun,int auto)
    {
        if (!is_jiauto)
        {
            int a = 12;
            for (int i = 0; i < 12; i++)
            {
                if (rotationz >= i * 30 + hun / 2 - 15 && rotationz < (i + 1) * 30 + hun / 2 - 15)
                {
                    a = i;
                    if (i == 0)
                        a = 12;
                    break;
                }
                else
                    a = 12;
            }
            if (a != ji)
            {
                Instantiate(ClockSE, new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                if (a != 12 && ji != 12)
                {
                    pitch += 0.05f * (a - ji);
                }
                if (a == gen.correctji)
                    idoltime = 0;
            }
            return a;
        }
        else
            return auto;
    }

    public int Returnhun(float rotationz, int auto)
    {
        if (!is_hunauto)
        {
            int a = 0;
            for (int i = 0; i < 12; i++)
            {
                if (rotationz >= i * 30 - 15 && rotationz < (i + 1) * 30 - 15)
                {
                    a = i;
                    break;
                }
                else
                    a = 0;
            }
            if (a * 5 != hun)
            {
                Instantiate(ClockSE, new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                if (a != 0 && hun != 0)
                {
                    pitch += 0.05f * (a - hun / 5);
                }
                if (a * 5 == gen.correcthun)
                    idoltime = 0;
            }
            return a * 5;
        }
        else
            return auto;
    }
}
