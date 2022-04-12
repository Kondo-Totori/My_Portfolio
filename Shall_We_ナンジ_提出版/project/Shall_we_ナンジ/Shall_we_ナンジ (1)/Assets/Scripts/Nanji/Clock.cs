using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField]
    float spownedtime, ususa, ran, realtime;
    public SpriteRenderer[] images;
    public TextMeshPro[] texts;
    ClockEditor editor;
    public int questji=12, questhun=0;
    public GameObject nagaihari, mijikaihari, hashira;
    public bool passed;
    bool ochiru;
    UIHolder UI;
    public float passtime = 10, clearedtime;
    public Sprite donut;

    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(-0.1f, 0.1f);
        editor = GameObject.Find("ClockEditor").GetComponent<ClockEditor>();
        UI = GameObject.Find("UIHolder").GetComponent<UIHolder>();
        switch (editor.questtype)
        {
            case 1:
                questji = Random.Range(1, 5)*3;
                questhun = 0;
                break;
            case 2:
                questji = Random.Range(1, 13);
                questhun = Random.Range(0, 4) * 15;
                break;
            case 3:
                questji = Random.Range(1, 13);
                questhun = Random.Range(0, 12) * 5;
                break;
        }
        if (UI.is_hardmode == 1 && editor.memoji != 0)
        {
            if (editor.rand < 0.5f)
                questhun = editor.memohun;
            else
                questji = editor.memoji;
        }
        realtime = Time.realtimeSinceStartup;
        if (UI.is_tutrial)
        {
            questji = 9;
            questhun = 15;
        }
        nagaihari.transform.rotation = Quaternion.Euler(0, 0, -questhun * 6);
        mijikaihari.transform.rotation = Quaternion.Euler(0, 0, -questji * 30 - questhun / 2);
        editor.mondaikimeru = false;
        if (UI.is_vsmode)
            hashira.transform.position = new Vector3(hashira.transform.position.x, hashira.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {


        if (UI.is_gamestart)
        {
            spownedtime += Time.deltaTime;
            clearedtime = Time.realtimeSinceStartup - realtime;
            foreach (SpriteRenderer a in images)
            {
                if (a.gameObject.name != "Cube (2)")
                    a.color = new Color(a.color.r, a.color.b, a.color.g, ususa);
            }
            foreach (TextMeshPro a in texts)
            {
                a.color = new Color(a.color.r, a.color.b, a.color.g, ususa);
            }

            if ((UI.gameLevel >= 4 && UI.gameLevel <= 6 && UI.turn == 1) || (UI.gameLevel2p >= 4 && UI.gameLevel2p <= 6 && UI.turn == 2))
            {
                if (spownedtime < passtime / 2)
                {
                    nagaihari.SetActive(false);
                    mijikaihari.SetActive(false);
                }
                else if (spownedtime < passtime / 2 + 1)
                {
                    nagaihari.SetActive(true);
                    mijikaihari.SetActive(true);
                    images[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, passtime / 2 + 1 - spownedtime);
                }
                else
                    images[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
            else if ((UI.gameLevel >= 7 && UI.gameLevel <= 9 && UI.turn == 1) || (UI.gameLevel2p >= 7 && UI.gameLevel2p <= 9 && UI.turn == 2))
            { 
                nagaihari.SetActive(false);
                mijikaihari.SetActive(false);
            }
            else if ((UI.gameLevel >= 10 && UI.gameLevel <= 12 && UI.turn == 1) || (UI.gameLevel2p >= 10 && UI.gameLevel2p <= 12 && UI.turn == 2))
            {
                /*if (spownedtime < passtime / 2)
                    ususa = 0;
                else if (spownedtime < passtime / 2 + 1)
                    ususa = spownedtime - passtime / 2;
                else
                    ususa = 1;*/
                images[0].sprite = donut;
                if (spownedtime < passtime / 2)
                {
                    nagaihari.SetActive(false);
                    mijikaihari.SetActive(false);
                    images[1].gameObject.SetActive(false);
                    images[2].gameObject.SetActive(false);
                    images[1].color = new Color(images[1].color.r, images[1].color.b, images[1].color.g, 0);
                    foreach (TextMeshPro a in texts)
                    {
                        a.gameObject.SetActive(false);
                        a.color = new Color(a.color.r, a.color.b, a.color.g, 0);
                    }
                }
                else if (spownedtime < passtime / 2 + 1)
                {
                    images[1].gameObject.SetActive(true);
                    images[2].gameObject.SetActive(true);
                    images[1].color = new Color(images[1].color.r, images[1].color.b, images[1].color.g, spownedtime - passtime / 2);
                    foreach (TextMeshPro a in texts)
                    {
                        a.gameObject.SetActive(true);
                        a.color = new Color(a.color.r, a.color.b, a.color.g, spownedtime - passtime / 2);
                    }
                }
                else
                {
                    images[1].color = new Color(images[1].color.r, images[1].color.b, images[1].color.g, ususa);
                }

            }

            if (spownedtime <= 1)
                //if (!((UI.gameLevel >= 10 && UI.gameLevel <= 12 && UI.turn == 1) || (UI.gameLevel2p >= 10 && UI.gameLevel2p <= 12 && UI.turn == 2)))
                    ususa = spownedtime;

            if (spownedtime < passtime)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 20 - spownedtime * 2 * (10 / passtime));
            }
            else
            {
                if (!passed)
                {
                    passed = true;
                    if (!editor.is_correct)
                    {
                        editor.damaged = true;
                        ochiru = true;
                        MoyaAudioManager.MoyaSE2 = true;
                        editor.LogEdit(editor.is_correct, 0);
                        if (UI.turn == 1)
                        {
                            if (UI.gameLevel >= 10)
                                UI.gameLevel -= 3;
                            else if (UI.gameLevel >= 7)
                                UI.gameLevel -= 2;
                            else if (UI.gameLevel != 1)
                                UI.gameLevel -= 1;
                            UI.combo = 0;
                        }
                        else
                        {
                            UI.combo2P = 0;
                            if (UI.gameLevel2p >= 10)
                                UI.gameLevel2p -= 3;
                            else if (UI.gameLevel2p >= 7)
                                UI.gameLevel2p -= 2;
                            else if (UI.gameLevel2p != 1)
                                UI.gameLevel2p -= 1;
                        }
                    }
                    else
                    {
                        MoyaAudioManager.MoyaSE = true;
                        UI.tubox.SetActive(false);
                        UI.is_tutrial = false;
                        UI.ComboAdd();
                        editor.LogEdit(editor.is_correct, 0);
                        if (UI.turn == 1)
                        {
                            if (clearedtime < 3)
                                UI.gameLevel += 3;
                            else if (clearedtime < 6)
                                UI.gameLevel += 2;
                            else if (clearedtime < 9)
                                UI.gameLevel += 1;

                            //ゲームの最大レベルを変えるときはここを変える
                            if (UI.gameLevel > UI.level_limit)
                                UI.gameLevel = UI.level_limit;
                        }
                        else
                        {
                            if (clearedtime < 3)
                                UI.gameLevel2p += 3;
                            else if (clearedtime < 6)
                                UI.gameLevel2p += 2;
                            else if (clearedtime < 9)
                                UI.gameLevel2p += 1;

                            //ゲームの最大レベルを変えるときはここを変える
                            if (UI.gameLevel2p > UI.level_limit)
                                UI.gameLevel2p = UI.level_limit;
                        }
                    }
                }
                images[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                if (ochiru)
                {
                    if (hashira != null)
                        Destroy(hashira);
                    transform.position = new Vector3(transform.position.x + ran, transform.position.y - Mathf.Pow((spownedtime - passtime), 2) * 0.5f, 0);
                    transform.Rotate(new Vector3(0, 0, ran * 30));
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, passtime * 15 - spownedtime * 15);
                    images[0].GetComponent<SpriteRenderer>().sortingOrder = 4;
                    images[1].GetComponent<SpriteRenderer>().sortingOrder = 5;
                    images[2].GetComponent<SpriteRenderer>().sortingOrder = 3;
                    foreach (TextMeshPro a in texts)
                    {
                        a.sortingOrder = 6;
                    }
                }
                ususa = (passtime + 1 - spownedtime) / 2;
            }
        }
        if (spownedtime > passtime + 3)
            Destroy(gameObject);
    }
}

