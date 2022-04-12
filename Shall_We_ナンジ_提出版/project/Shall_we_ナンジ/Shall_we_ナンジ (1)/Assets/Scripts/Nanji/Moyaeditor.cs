using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moyaeditor : MonoBehaviour {


    public GameObject moyasource;
    public float cooltime;
    public int moyakazu;
    float time;
    GameObject source;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > cooltime&&moyakazu==0)
        {
            time = 0;
            float posx, posy;
            posx = Random.Range(-209.937f, 209.937f);
            posy = Random.Range(-Mathf.Sqrt(Mathf.Pow(209.937f, 2) - Mathf.Pow(posx, 2)), Mathf.Sqrt(Mathf.Pow(209.937f, 2) - Mathf.Pow(posx, 2)));
            source = Instantiate(moyasource, new Vector2(posx + 937.736f, posy + 390.560f), Quaternion.identity);
            source.transform.parent = GameObject.Find("Moyas").transform;
            moyakazu++;
            source.GetComponent<Moya>().moyaNumber = Random.Range(1, 13);
        }
    }
}
