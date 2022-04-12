using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRound : MonoBehaviour
{

    public GameObject[] hari;
    public int ji, hun;
    public float rotji, rothun;
    public Round rou;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        rotji = rou.rotji;
        rothun = rou.rothun;
        //針がマイナスの値にならないようにする

        if (rotji < 0)
        {
            rotji += 360;
        }
        if (rotji >= 360)
        {
            rotji -= 360;
        }
        if (rothun < 0)
        {
            rothun += 360;
        }
        if (rothun >= 360)
        {
            rothun -= 360;
        }

        hari[0].transform.rotation = Quaternion.Euler(0, 0, -rotji);
        hari[1].transform.rotation = Quaternion.Euler(0, 0, -rothun);

        //時と分の取得
        hun = Returnhun(rothun);
        ji = Returnji(rotji, hun);
    }

    int Returnji(float rotationz, int hun)
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
        return a;
    }

    int Returnhun(float rotationz)
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
        return a * 5;
    }
}
