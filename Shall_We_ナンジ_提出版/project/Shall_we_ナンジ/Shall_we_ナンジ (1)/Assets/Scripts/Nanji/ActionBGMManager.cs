using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBGMManager : MonoBehaviour
{
    public AudioSource BGM1, BGM2;
    public float BGMpitch;

    // Start is called before the first frame update
    void Start() { 
        BGM1.Play();
        BGM2.PlayDelayed(5.33f);
    }

    // Update is called once per frame
    void Update()
    {
        BGM1.pitch = BGMpitch;
        BGM2.pitch = BGMpitch;
    }
}
