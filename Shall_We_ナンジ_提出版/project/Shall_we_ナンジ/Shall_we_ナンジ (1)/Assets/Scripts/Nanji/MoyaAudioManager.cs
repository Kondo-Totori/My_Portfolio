using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoyaAudioManager : MonoBehaviour
{
    public static bool MoyaSE,MoyaSE2,MoyaSE3;
    public AudioClip[] SEs;
    AudioSource SEsource;

    // Start is called before the first frame update
    void Start()
    {
        SEsource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoyaSE)
        {
            MoyaSE = false;
            SEsource.volume = 1f;
            SEsource.clip = SEs[Random.Range(0,SEs.Length-2)];
            SEsource.Play();
        }
        if(MoyaSE2)
        {
            MoyaSE2 = false;
            SEsource.volume = 1f;
            SEsource.clip = SEs[SEs.Length-2];
            SEsource.Play();
        }
        if (MoyaSE3)
        {
            MoyaSE3 = false;
            SEsource.volume = 0.2f;
            SEsource.clip = SEs[SEs.Length - 1];
            SEsource.Play();
        }
    }
}
