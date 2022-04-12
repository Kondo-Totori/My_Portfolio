using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Moya : MonoBehaviour
{

    public int moyaNumber;
    Round round;
    public bool is_jiatteru;
    public TextMeshProUGUI number;
    public int hp=60;
    // Start is called before the first frame update
    void Start()
    {
        number.text = moyaNumber.ToString();
        round = GameObject.Find("point").GetComponent<Round>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moyaNumber == round.ji)
            is_jiatteru = true;
        else
            is_jiatteru = false;


    }
}
