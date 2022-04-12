using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSource : MonoBehaviour {

    float destroytimer,limit=0.3f;
    private GameObject child_;
    public int layer=1;
    bool layered;


	// Use this for initialization
	void Start () {
        if (Time.timeScale == 0)
            Destroy(gameObject);
        if (Time.timeScale == 3)
            limit = 0.4f;
        else
            limit = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        destroytimer += Time.deltaTime;
        foreach(Transform t in transform)
        {
            t.gameObject.layer = layer;
            layered = true;
        }
        if(layered)
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, limit - destroytimer);
        if (destroytimer > limit)
            Destroy(gameObject);
	}
}
