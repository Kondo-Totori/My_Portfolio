using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumSashier : MonoBehaviour {

    public TextMeshProUGUI[] level1, level2, level3;
    public int level=1,most,worst,mostnum,worstnum,seikaisu,selectmost,selectworst;
    int[] nums = new int[8];
    public AnyGameController[] hands;

	// Use this for initialization
	void Start () {
        MakeQuestion();
	}
	
	// Update is called once per frame
	void Update () {
        switch (level)
        {
            case 1:
                if ((hands[0].control_return >= 1 && hands[0].control_return <= 6) || (hands[0].control_return >= 19 && hands[0].control_return <= 24))
                    selectmost = 0;
                else
                    selectmost = 1;

                if ((hands[1].control_return >= 1 && hands[1].control_return <= 6) || (hands[1].control_return >= 19 && hands[1].control_return <= 24))
                    selectworst = 0;
                else
                    selectworst = 1;
                break;
            case 2:
                if ((hands[0].control_return >= 1 && hands[0].control_return <= 3) || (hands[0].control_return >= 22 && hands[0].control_return <= 24))
                    selectmost = 0;
                else if (hands[0].control_return >= 4 && hands[0].control_return <= 9)
                    selectmost = 1;
                else if (hands[0].control_return >= 10 && hands[0].control_return <= 15)
                    selectmost = 2;
                else
                    selectmost = 3;

                if ((hands[1].control_return >= 1 && hands[1].control_return <= 3) || (hands[1].control_return >= 22 && hands[1].control_return <= 24))
                    selectworst = 0;
                else if (hands[1].control_return >= 4 && hands[1].control_return <= 9)
                    selectworst = 1;
                else if (hands[1].control_return >= 10 && hands[1].control_return <= 15)
                    selectworst = 2;
                else
                    selectworst = 3;
                break;
            case 3:
                if ((hands[0].control_return >= 1 && hands[0].control_return <= 2) || hands[0].control_return == 22)
                    selectmost = 0;
                else if (hands[0].control_return >= 3 && hands[0].control_return <= 5)
                    selectmost = 1;
                else if (hands[0].control_return >= 6 && hands[0].control_return <= 8)
                    selectmost = 2;
                else if (hands[0].control_return >= 9 && hands[0].control_return <= 11)
                    selectmost = 3;
                else if (hands[0].control_return >= 12 && hands[0].control_return <= 14)
                    selectmost = 4;
                else if (hands[0].control_return >= 15 && hands[0].control_return <= 17)
                    selectmost = 5;
                else if (hands[0].control_return >= 18 && hands[0].control_return <= 20)
                    selectmost = 6;
                else
                    selectmost = 7;

                if ((hands[1].control_return >= 1 && hands[1].control_return <= 2) || hands[1].control_return == 22)
                    selectworst = 0;
                else if (hands[1].control_return >= 3 && hands[1].control_return <= 5)
                    selectworst = 1;
                else if (hands[1].control_return >= 6 && hands[1].control_return <= 8)
                    selectworst = 2;
                else if (hands[1].control_return >= 9 && hands[1].control_return <= 11)
                    selectworst = 3;
                else if (hands[1].control_return >= 12 && hands[1].control_return <= 14)
                    selectworst = 4;
                else if (hands[1].control_return >= 15 && hands[1].control_return <= 17)
                    selectworst = 5;
                else if (hands[1].control_return >= 18 && hands[1].control_return <= 20)
                    selectworst = 6;
                else
                    selectworst = 7;
                break;
            default:
                break;
        }

        if (selectmost == mostnum && selectworst == worstnum)
            Correct();
	}

    private void Reset()
    {
        level = 1;
    }

    void Correct()
    {
        seikaisu++;
        if (seikaisu > 3)
            level = 2;
        if (seikaisu > 5)
            level = 3;
        MakeQuestion();
    }

    void MakeQuestion()
    {
        List<int> numbers = new List<int>();
        int index;
        most = 0;
        worst = 10;
        for (int i = 1; i < 10; i++)
        {
            numbers.Add(i);
        }

        level1[0].text = "";
        level1[1].text = "";
        level2[0].text = "";
        level2[1].text = "";
        level3[0].text = "";
        level3[1].text = "";
        level3[2].text = "";
        level3[3].text = "";
        for (int i = 0; i < Mathf.Pow(2,level); i++)
        {
            index = Random.Range(0, numbers.Count);
            nums[i] = numbers[index];
            if (numbers[index] > most)
            {
                most = numbers[index];
                mostnum = i;
            }
            if (numbers[index] < worst)
            {
                worst = numbers[index];
                worstnum = i;
            }
            numbers.RemoveAt(index);
        }
        switch (level)
        {
            case 1:
                level1[0].text = nums[0] + "";
                level1[1].text = nums[1] + "";
                break;
            case 2:
                level1[0].text = nums[0] + "";
                level2[1].text = nums[1] + "";
                level1[1].text = nums[2] + "";
                level2[0].text = nums[3] + "";
                break;
            case 3:
                level1[0].text = nums[0] + "";
                level3[3].text = nums[1] + "";
                level2[1].text = nums[2] + "";
                level3[1].text = nums[3] + "";
                level1[1].text = nums[4] + "";
                level3[2].text = nums[5] + "";
                level2[0].text = nums[6] + "";
                level3[0].text = nums[7] + "";
                break;
        }
    }
}
