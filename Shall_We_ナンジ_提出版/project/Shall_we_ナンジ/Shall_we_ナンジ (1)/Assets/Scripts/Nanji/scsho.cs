using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scsho : MonoBehaviour {
    DateTime now;
    
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        now = DateTime.Now;
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath+"/ScreenShot/Gestao "+now.Year+"-"+now.Month+"-"+now.Day+"-"+now.Hour+"-"+now.Minute+"-"+now.Second+".png");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
