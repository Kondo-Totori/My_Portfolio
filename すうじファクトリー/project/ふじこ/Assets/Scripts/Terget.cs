using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terget : MonoBehaviour
{
    [SerializeField]
    Text target;
    public float my_number;
    [SerializeField]
    Animation point;
    [SerializeField]
    Text pointText;
    [SerializeField]
    main Main;
    private int count;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget()
    {
        my_number = Random.Range(7, 15);
        target.text = my_number.ToString();
        count = 0;
    }

    public void match(float playerNumber)
    {
        if (playerNumber == my_number)
        {
            Main.AddCombo();
            PointShow(1);
            BGMManager.f_point = true;

        }
        else if (Mathf.Abs(playerNumber - my_number) < 2)
        {
            //ニアミス時処理
            PointShow(2);
            BGMManager.f_point = true;
        }
        else
        {
            BGMManager.f_incorrect=true;
            Main.ResetCombo();
        }
        count++;
        if (count >= 10)
            Main.ResultAnime();
    }

    public void PointShow(int miss)
    {
        int pScore=Main.ScoreCalculate();
        Main.AddScore(pScore/miss);
        pointText.text = "+" + (pScore / miss);
        point.Play("point");
    }


}
