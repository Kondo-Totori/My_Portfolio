using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackChanger : MonoBehaviour {

    Camera cam;
    public bool correct_anime, incorrect_anime;
    public Color seikaicolor, huseikaicolor,basecolor;

    // Use this for initialization
    void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (correct_anime)
            cam.backgroundColor = seikaicolor;
        else if (incorrect_anime)
            cam.backgroundColor = huseikaicolor;
        else
            cam.backgroundColor = basecolor;
    }
}
