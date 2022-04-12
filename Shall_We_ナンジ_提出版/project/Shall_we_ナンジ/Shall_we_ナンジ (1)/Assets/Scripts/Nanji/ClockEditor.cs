using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class ClockEditor : MonoBehaviour
{
    public GameObject clockSource,clockSource2P,shuchu,slow,slow2p;
    public float questtime=0,a, rand;
    public bool is_correct,damaged,mondaikimeru;
    public GameObject clock;
    float tscale, terget,cameratime,kasokuBonus=3,passtime=10,windvolume,objtime;
    public TextMeshProUGUI jikoku;
    Round point;
    public Camera cam,cam2p;
    UIHolder UI;
    public ActionBGMManager BGMmanager;
    int faintji, fainthun, faintkekka, faintkekka2, kuri = 0;
    string fileName;
    public int questtype, memoji = 0, memohun = 0;
    public AudioSource wind;
    public GameObject testObj;

    // Start is called before the first frame update
    void Start()
    {
        questtype = 1;
        windvolume = 0;
        point = GameObject.Find("SpritePoint").GetComponent<Round>();
        UI = GameObject.Find("UIHolder").GetComponent<UIHolder>();
        if (UI.is_vsmode == true)
        {
            cam.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            cam2p.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        slow2p.SetActive(false);
        fileName=DateTime.Now.Month.ToString("D2")+"-"+DateTime.Now.Day.ToString("D2") + "-" +UIHolder.playerID.ToString("D2");
        LogEdit(false, 1);
    }

    // Update is called once per frame
    void Update()
    {
        wind.volume = windvolume;
        if (UI.is_gamestart)
        {
            if (!UI.is_tutrial && point.smallacttime > 3)
                UI.is_smallaction = true;
            else
                UI.is_smallaction = false;
            if (UI.score < 3000)
                questtype = 1;
            else if (UI.score < 6000)
                questtype = 2;
            else
                questtype = 3;

            if (!is_correct && questtime > passtime - 1 && questtime <= passtime)
            {
                if (UI.is_tutrial)
                    terget = 0f;
                else
                    terget = 0.3f;
                if (UI.turn == 1)
                    slow.SetActive(true);
                else
                    slow2p.SetActive(true);
                shuchu.SetActive(false);
                if (BGMmanager.BGMpitch == 1)
                    BGMmanager.BGMpitch = 0.8f;
                if (BGMmanager.BGMpitch > 0.5f)
                    BGMmanager.BGMpitch -= 0.01f;
                else
                    BGMmanager.BGMpitch = 0.5f;
            }
            else
            {
                if (BGMmanager.BGMpitch < 1)
                    BGMmanager.BGMpitch += 0.02f;
                else
                    BGMmanager.BGMpitch = 1;
                terget = 1;
                if (UI.turn == 1)
                    slow.SetActive(false);
                else
                    slow2p.SetActive(false);
                shuchu.SetActive(false);
                if (is_correct && !UI.homeruAnime.isPlaying)
                {
                    if (!UI.is_tutrial)
                    {
                        kasokuBonus -= Time.deltaTime;
                    }
                    terget = 3;
                    shuchu.SetActive(true);
                    if (windvolume < 1)
                    {
                        windvolume += 0.2f;
                    }
                }
                else if (windvolume > 0)
                    windvolume -= 0.4f;
            }

            if (kasokuBonus < 0)
            {
                kasokuBonus = 3;
                if(UI.turn==1)
                  UI.ScoreAdd(100+UI.combo*20);
                else
                  UI.ScoreAdd(100 + UI.combo2P * 20);
            }

            if (slow.active||slow2p.active)
            {
                if (a < 0.39)
                    a += 0.02f;
            }
            else
                a = 0;

            if (UI.turn == 1)
            {
                slow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
                if (UI.is_vsmode)
                    shuchu.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
            else
            {
                slow2p.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
                shuchu.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }


            if (tscale > terget || tscale < terget)
                tscale = terget;

            Time.timeScale = tscale;
            questtime += Time.deltaTime;
            if (questtime > passtime + 2 && clock.GetComponent<Clock>().passed)
            {
                questtime = 0;
                if (UI.turn == 1 && UI.is_vsmode)
                    UI.turn = 2;
                else
                    UI.turn = 1;
                if (UI.turn == 1)
                {
                    clock = Instantiate(clockSource, new Vector3(transform.position.x, transform.position.y, 20), Quaternion.identity);
                    clock.transform.parent = GameObject.Find("ClockEditor").transform;
                }
                else
                {
                    clock = Instantiate(clockSource2P, new Vector3(transform.position.x, transform.position.y, 20), Quaternion.identity);
                    clock.transform.parent = GameObject.Find("ClockEditor").transform;
                }
                if (UI.turn == 1)
                {
                    switch (UI.gameLevel)
                    {
                        case 1:
                        case 10:
                        case 13:
                            clock.GetComponent<Clock>().passtime = 10;
                            break;
                        case 2:
                        case 11:
                        case 14:
                            clock.GetComponent<Clock>().passtime = 7.5f;
                            break;
                        case 4:
                            clock.GetComponent<Clock>().passtime = 9;
                            break;
                        case 5:
                            clock.GetComponent<Clock>().passtime = 7;
                            break;
                        case 7:
                            clock.GetComponent<Clock>().passtime = 8;
                            break;
                        case 8:
                            clock.GetComponent<Clock>().passtime = 6.5f;
                            break;
                        case 3:
                        case 6:
                        case 9:
                        case 12:
                        case 15:
                            clock.GetComponent<Clock>().passtime = 5;
                            break;
                    }
                }
                else
                {
                    switch (UI.gameLevel2p)
                    {
                        case 1:
                        case 10:
                        case 13:
                            clock.GetComponent<Clock>().passtime = 10;
                            break;
                        case 2:
                        case 11:
                        case 14:
                            clock.GetComponent<Clock>().passtime = 7.5f;
                            break;
                        case 4:
                            clock.GetComponent<Clock>().passtime = 9;
                            break;
                        case 5:
                            clock.GetComponent<Clock>().passtime = 7;
                            break;
                        case 7:
                            clock.GetComponent<Clock>().passtime = 8;
                            break;
                        case 8:
                            clock.GetComponent<Clock>().passtime = 6.5f;
                            break;
                        case 3:
                        case 6:
                        case 9:
                        case 12:
                        case 15:
                            clock.GetComponent<Clock>().passtime = 5;
                            break;
                    }
                }
                passtime = clock.GetComponent<Clock>().passtime;
            }
            if (point.is_hunauto)
            {
                point.hun = point.Returnhun(point.rothun, clock.GetComponent<Clock>().questhun);
            }
            else
                point.hun = point.Returnhun(point.rothun, 0);
            if (!UI.is_vsmode)
            {
                if (point.is_jiauto)
                    point.ji = point.Returnji(point.rotji, clock.GetComponent<Clock>().questhun, clock.GetComponent<Clock>().questji);
                else
                    point.ji = point.Returnji(point.rotji, point.hun, 0);
            }
            else
                point.ji = point.Returnji(point.rotji, clock.GetComponent<Clock>().questhun, 0);

            if (UI.is_vsmode)
            {
                if (UI.turn==1 && clock.GetComponent<Clock>().questhun == point.hun)
                    is_correct = true;
                else if (clock.GetComponent<Clock>().questji == point.ji && UI.turn==2)
                    is_correct = true;
                else
                    is_correct = false;
            }
            else
            {
                if (clock.GetComponent<Clock>().questji == point.ji && clock.GetComponent<Clock>().questhun == point.hun)
                    is_correct = true;
                else
                    is_correct = false;
            }
            if (!mondaikimeru) {
                MoyaAudioManager.MoyaSE3 = true;
                mondaikimeru = true;
                if (UI.turn == 1)
                {
                    if (UI.is_vsmode)
                    {
                        jikoku.fontSize = 100;
                        jikoku.gameObject.transform.position = new Vector3(475, 910, 0);
                    }
                    if ( memoji == 0 && memohun == 0)
                    {
                        jikoku.gameObject.transform.localPosition = new Vector3(0, 320, 0);
                        jikoku.fontSize = 150;
                        jikoku.alignment = TextAlignmentOptions.Center;
                        jikoku.lineSpacing = 0;
                        jikoku.text = "<color=blue>"+clock.GetComponent<Clock>().questji.ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2")+"</color>";
                        jikoku.gameObject.GetComponent<Animation>().Play();
                        if (UI.is_hardmode != 0)
                        {
                            memoji = clock.GetComponent<Clock>().questji;
                            memohun = clock.GetComponent<Clock>().questhun;
                            rand = UnityEngine.Random.Range(0f, 1f);
                        }
                    }
                    else
                    {
                        if (UI.is_hardmode==1 && memoji != 0 )
                        {
                            jikoku.gameObject.transform.localPosition = new Vector3(-64, 320, 0);
                            jikoku.fontSize = 120;
                            jikoku.alignment = TextAlignmentOptions.Right;
                            jikoku.lineSpacing = -50;
                            if (rand < 0.25f)
                            {
                                //faintji = UnityEngine.Random.Range(1, 13-clock.GetComponent<Clock>().questji);
                                faintji = memoji-clock.GetComponent<Clock>().questji;
                                if ((faintji) < 0)
                                {
                                    if((faintji + clock.GetComponent<Clock>().questji)<0)
                                        jikoku.text = "<color=blue>" + ((faintji + clock.GetComponent<Clock>().questji) + 12).ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2") + "</color>\n-<color=blue>" + (faintji + 12).ToString("D2") + "</color>：<color=red>00" + "</color>";
                                    else
                                        jikoku.text = "<color=blue>" + ((faintji + clock.GetComponent<Clock>().questji)).ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2") + "</color>\n-<color=blue>" + (faintji + 12).ToString("D2") + "</color>：<color=red>00" + "</color>";
                                }
                                else
                                    jikoku.text = "<color=blue>" + ((faintji + clock.GetComponent<Clock>().questji)).ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2") + "</color>\n-<color=blue>" + faintji.ToString("D2") + "</color>：<color=red>00" + "</color>";
                            }
                            else if (rand < 0.5f)
                            {
                                //faintji = UnityEngine.Random.Range(1, clock.GetComponent<Clock>().questji);
                                faintji = -memoji + clock.GetComponent<Clock>().questji;
                                if (faintji < 1)
                                    faintji += 12;
                                if((-faintji + clock.GetComponent<Clock>().questji)<1)
                                    jikoku.text = "<color=blue>" + ((-faintji + clock.GetComponent<Clock>().questji)+12).ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2") + "</color>\n+<color=blue>" + faintji.ToString("D2") + "</color>：<color=red>00" + "</color>";
                                else
                                    jikoku.text = "<color=blue>" + ((-faintji + clock.GetComponent<Clock>().questji)).ToString("D2") + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun.ToString("D2") + "</color>\n+<color=blue>" + faintji.ToString("D2") + "</color>：<color=red>00" + "</color>";
                            }
                            else if (rand < 0.75f)
                            {
                                //fainthun = UnityEngine.Random.Range(1, 12 - (clock.GetComponent<Clock>().questhun) / 5) * 5;
                                fainthun = memohun - clock.GetComponent<Clock>().questhun;
                                if (fainthun > 59)
                                    fainthun -= 60;
                                if (fainthun < 0)
                                    jikoku.text = "<color=blue>" + clock.GetComponent<Clock>().questji.ToString("D2") + "</color>：<color=red>" + ((fainthun + clock.GetComponent<Clock>().questhun) % 60).ToString("D2") + "</color>\n+<color=blue>00</color>：<color=red>" + (fainthun*-1).ToString("D2") + "</color>";
                                else
                                    jikoku.text = "<color=blue>" + clock.GetComponent<Clock>().questji.ToString("D2") + "</color>：<color=red>" + ((fainthun + clock.GetComponent<Clock>().questhun)%60).ToString("D2") + "</color>\n-<color=blue>00</color>：<color=red>" + fainthun.ToString("D2") + "</color>";
                            }
                            else
                            {
                                //fainthun = UnityEngine.Random.Range(1, clock.GetComponent<Clock>().questji / 5) * 5;
                                fainthun = -memohun + clock.GetComponent<Clock>().questhun;
                                if (fainthun < 0)
                                    fainthun += 60;
                                if ((-fainthun + clock.GetComponent<Clock>().questhun) < 0) {
                                        jikoku.text = "<color=blue>" + (clock.GetComponent<Clock>().questji).ToString("D2") + "</color>：<color=red>" + ((-fainthun + clock.GetComponent<Clock>().questhun)+60).ToString("D2") + "</color>\n-<color=blue>00</color>：<color=red>" + ((fainthun-60)*-1).ToString("D2") + "</color>";
                                }
                                else
                                    jikoku.text = "<color=blue>" + clock.GetComponent<Clock>().questji.ToString("D2") + "</color>：<color=red>" + ((-fainthun + clock.GetComponent<Clock>().questhun) % 60).ToString("D2") + "</color>\n+<color=blue>00</color>：<color=red>" + fainthun.ToString("D2") + "</color>";
                            }
                            jikoku.gameObject.GetComponent<Animation>().Play();
                            memoji = 0;
                            memohun = 0;
                        }
                        if (UI.is_hardmode == 2&&memoji!=0)
                        {
                            jikoku.gameObject.transform.localPosition = new Vector3(-64, 320, 0);
                            jikoku.fontSize = 120;
                            jikoku.alignment = TextAlignmentOptions.Right;
                            jikoku.lineSpacing = -50;
                            kuri = 0;
                            if (rand < 0.5f)
                            {
                                //fainthun = UnityEngine.Random.Range(1, 12) * 5;
                                fainthun = memohun - clock.GetComponent<Clock>().questhun;
                                faintkekka2 = (fainthun + clock.GetComponent<Clock>().questhun) % 60;
                                if (memohun < clock.GetComponent<Clock>().questhun)
                                {
                                    fainthun += 60;
                                    kuri = 1;
                                }
                                //faintji = UnityEngine.Random.Range(1, 13);
                                faintji = memoji - kuri - clock.GetComponent<Clock>().questji;
                                faintkekka = (faintji + kuri + clock.GetComponent<Clock>().questji);
                                if (faintkekka > 12)
                                    faintkekka -= 12;
                                if (faintkekka < 1)
                                    faintkekka += 12;
                                if (faintji < 1)
                                    faintji += 12;
                                if (fainthun < 0)
                                {
                                    faintji -= 1;
                                    fainthun += 60;
                                }
                                jikoku.text = "<color=blue>" + (memoji).ToString("D2") + "</color>：<color=red>" + (memohun).ToString("D2") + "</color>\n-<color=blue>" + faintji.ToString("D2") + "</color>：<color=red>"+fainthun.ToString("D2")+"</color>";
                            }
                            else
                            {
                                //faintji = UnityEngine.Random.Range(1, 13);
                                //fainthun = UnityEngine.Random.Range(1, 12) * 5;
                                fainthun = clock.GetComponent<Clock>().questhun- memohun;
                                faintkekka2 = (-fainthun + clock.GetComponent<Clock>().questhun) % 60;
                                if (memohun > clock.GetComponent<Clock>().questhun)
                                {
                                    fainthun += 60;
                                    kuri = 1;
                                }
                                //faintji = UnityEngine.Random.Range(1, 13);
                                faintji = -memoji - kuri + clock.GetComponent<Clock>().questji;
                                faintkekka = (-faintji + kuri + clock.GetComponent<Clock>().questji);
                                if (faintkekka > 12)
                                    faintkekka -= 12;
                                if (faintkekka < 1)
                                    faintkekka += 12;
                                if (faintji < 1)
                                    faintji += 12;
                                if (fainthun < 0)
                                {
                                    faintji -= 1;
                                    fainthun += 60;
                                }
                                jikoku.text = "<color=blue>" + (memoji).ToString("D2") + "</color>：<color=red>" + (memohun).ToString("D2") + "</color>\n+<color=blue>" + faintji.ToString("D2") + "</color>：<color=red>" + fainthun.ToString("D2")+"</color>";
                            }
                            jikoku.gameObject.GetComponent<Animation>().Play();
                        }
                        memoji = 0;
                        memohun = 0;
                    }
                }
                else
                {
                    if (UI.is_vsmode)
                        jikoku.gameObject.transform.position = new Vector3(1425, 910, 0);
                    //if (UI.gameLevel2p <= 9)
                        jikoku.text = "<color=blue>" + clock.GetComponent<Clock>().questji + "</color>：<color=red>" + clock.GetComponent<Clock>().questhun+"</color>";
                        jikoku.gameObject.GetComponent<Animation>().Play();
                    /*else
                    {
                        if (UI.gameLevel2p >= 10 && UI.gameLevel2p <= 12)
                        {
                            rand = UnityEngine.Random.Range(0f, 1f);
                            if (rand < 0.25f)
                            {
                                faintji = UnityEngine.Random.Range(1, 13 - clock.GetComponent<Clock>().questji);
                                jikoku.text = (faintji + clock.GetComponent<Clock>().questji) + "<size=54>時</size>" + clock.GetComponent<Clock>().questhun + "<size=54>分の</size>" + faintji + "<size=54>時間前</size>";
                            }
                            else if (rand < 0.5f)
                            {
                                faintji = UnityEngine.Random.Range(1, clock.GetComponent<Clock>().questji);
                                jikoku.text = (-faintji + clock.GetComponent<Clock>().questji) + "<size=54>時</size>" + clock.GetComponent<Clock>().questhun + "<size=54>分の</size>" + faintji + "<size=54>時間後</size>";
                            }
                            else if (rand < 0.75f)
                            {
                                fainthun = UnityEngine.Random.Range(1, 12 - (clock.GetComponent<Clock>().questhun) / 5) * 5;
                                jikoku.text = clock.GetComponent<Clock>().questji + "<size=54>時</size>" + (fainthun + clock.GetComponent<Clock>().questhun) + "<size=54>分の</size>" + fainthun + "<size=54>分前</size>";
                            }
                            else
                            {
                                fainthun = UnityEngine.Random.Range(1, clock.GetComponent<Clock>().questji / 5) * 5;
                                jikoku.text = clock.GetComponent<Clock>().questji + "<size=54>時</size>" + (-fainthun + clock.GetComponent<Clock>().questhun) + "<size=54>分の</size>" + fainthun + "<size=54>分後</size>";
                            }
                        }
                        if (UI.gameLevel2p>= 13 && UI.gameLevel2p <= 15)
                        {
                            kuri = 0;
                            rand = UnityEngine.Random.Range(0f, 1f);
                            if (rand < 0.25f)
                            {
                                faintji = UnityEngine.Random.Range(1, 13);
                                faintkekka = (faintji + clock.GetComponent<Clock>().questji) % 12;
                                jikoku.text = faintkekka + "<size=54>時</size>" + clock.GetComponent<Clock>().questhun + "<size=54>分の</size>" + faintji + "<size=54>時間前</size>";
                            }
                            else if (rand < 0.5f)
                            {
                                faintji = UnityEngine.Random.Range(1, 13);
                                faintkekka = (-faintji + clock.GetComponent<Clock>().questji);
                                if (faintkekka < 0)
                                    faintkekka += 12;
                                jikoku.text = faintkekka + "<size=54>時</size>" + clock.GetComponent<Clock>().questhun + "<size=54>分の</size>" + faintji + "<size=54>時間後</size>";
                            }
                            else if (rand < 0.75f)
                            {
                                fainthun = UnityEngine.Random.Range(1, 12) * 5;
                                faintkekka = (fainthun + clock.GetComponent<Clock>().questhun) % 60;
                                if ((fainthun + clock.GetComponent<Clock>().questhun) / 60 > 0)
                                    kuri = 1;
                                jikoku.text = clock.GetComponent<Clock>().questji + kuri + "<size=54>時</size>" + faintkekka + "<size=54>分の</size>" + fainthun + "<size=54>分前</size>";
                            }
                            else
                            {
                                fainthun = UnityEngine.Random.Range(1, 12) * 5;
                                faintkekka = (-fainthun + clock.GetComponent<Clock>().questhun);
                                if (faintkekka < 0)
                                {
                                    faintkekka += 60;
                                    kuri = -1;
                                }
                                jikoku.text = clock.GetComponent<Clock>().questji + kuri + "<size=54>時</size>" + faintkekka + "<size=54>分の</size>" + fainthun + "<size=54>分後</size>";
                            }
                        }
                        Debug.Log(clock.GetComponent<Clock>().questhun + "," + fainthun);
                    }*/
                }
            }
            if (damaged)
            {
                damaged = false;
                cameratime = 2f;
            }
            if (cameratime > 0)
            {
                if (UI.turn == 1)
                {
                    if (cameratime > 1f)
                        cam.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-0.5f * (cameratime - 1), 0.5f * (cameratime - 1)), UnityEngine.Random.Range(-0.5f * (cameratime - 1), 0.5f * (cameratime - 1)), -10);
                    else
                        cam.gameObject.transform.position = new Vector3(0, 0, -10);
                    cam.backgroundColor = new Color(cameratime / 2, 1 - cameratime / 4, 1 - cameratime / 4);
                }
                else
                {
                    if (cameratime > 1f)
                        cam2p.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-0.5f * (cameratime - 1), 0.5f * (cameratime - 1)), UnityEngine.Random.Range(-0.5f * (cameratime - 1), 0.5f * (cameratime - 1)), -10);
                    else
                        cam2p.gameObject.transform.position = new Vector3(0, 0, -10);
                    cam2p.backgroundColor = new Color(cameratime / 2, 1 - cameratime / 4, 1 - cameratime / 4);
                }
                cameratime -= Time.deltaTime;
            }
            else
            {
                cam.gameObject.transform.position = new Vector3(0, 0, -10);
                cam2p.gameObject.transform.position = new Vector3(0, 0, -10);
            }
            objtime += Time.deltaTime;
            if (objtime > 0.2f)
            {
                objtime = 0;
                Objectgenerator(testObj);
            }
        }
    }

    public void LogEdit(bool correct, int mode)
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Log/" + fileName + ".csv");
        sw = fi.AppendText();
        if (mode == 0)
            sw.WriteLine(EditMessage(correct));
        else if(mode==2)
            sw.WriteLine(EditMessage(2));
        else
            sw.WriteLine("レベル,問題,回答,正誤,回答時間,スコア");
        sw.Flush();
        sw.Close();
    }

    public string EditMessage(bool correct)
    {
        string messe;
        messe = UI.gameLevel + "," + clock.GetComponent<Clock>().questji + "時" + clock.GetComponent<Clock>().questhun + "分," + point.ji + "時" + point.hun + "分,";
        if (correct)
            messe += "○,";
        else
            messe += "✕,";
        messe += clock.GetComponent<Clock>().clearedtime.ToString("F2") + "," + UI.score;
        return messe;
    }

    public string EditMessage(int correct)
    {
        string messe;
        messe = UI.gameLevel + "," + clock.GetComponent<Clock>().questji + "時" + clock.GetComponent<Clock>().questhun + "分," + point.ji + "時" + point.hun + "分,--,--," + UI.score+",タイムアップ";
        return messe;
    }

    public void Objectgenerator(GameObject obj)
    {
        float x, y;
        x = UnityEngine.Random.Range(7f, 15f);
        y = UnityEngine.Random.Range(-5f, 5f);
        Instantiate(obj, new Vector3(x, y, 20), Quaternion.identity);
        Instantiate(obj, new Vector3(-x, y, 20), Quaternion.identity);
    }
}
