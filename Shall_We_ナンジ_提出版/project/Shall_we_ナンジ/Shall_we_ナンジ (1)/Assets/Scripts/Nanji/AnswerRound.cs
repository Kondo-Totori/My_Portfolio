using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerRound : MonoBehaviour {

    public GameObject nagaihari, mijikaihari;
    public bool is_set,is_set2;
    float scales = 1,animetime, animetime2;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (is_set)
        {
            animetime += Time.deltaTime;
            scales = 1f + Mathf.Sin(animetime*6*Mathf.PI)*0.3f;
        }
        if (animetime > 1&&is_set)
        {
            is_set = false;
            animetime = 0;
        }

        if (is_set2)
        {
            animetime2 += Time.deltaTime;
            scales = 1f + Mathf.Sin(animetime2* Mathf.PI) * 0.05f;
        }
        nagaihari.transform.localScale = new Vector3(scales, scales, scales);
        mijikaihari.transform.localScale = new Vector3(scales, scales, scales);
    }

    public void SetAnswer(int ji,int hun)
    {
        nagaihari.transform.rotation = Quaternion.Euler(0, 0, -hun*6);
        mijikaihari.transform.rotation = Quaternion.Euler(0, 0, -ji * 30 - hun / 2);
    }

}
