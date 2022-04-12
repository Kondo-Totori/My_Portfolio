using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static bool questSE, correctSE, incorrectSE;
    public static float BGMpitch=1,BGMvolume=1 ,SEvolume=1;
    public AudioSource SEsoource, BGMsource;
    public AudioClip[] SEs;
    bool BGMmodoru;
    float mutetime;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (questSE)
        {
            questSE = false;
            BGMmodoru = true;
            mutetime = 0;
            SEsoource.clip = SEs[0];
            SEsoource.Play();
        }
        if (correctSE)
        {
            correctSE = false;
            BGMmodoru = true;
            mutetime = 0;
            SEsoource.clip = SEs[1];
            SEsoource.Play();
        }
        if (incorrectSE)
        {
            incorrectSE = false;
            BGMmodoru = true;
            mutetime = 0;
            SEsoource.clip = SEs[2];
            SEsoource.Play();
        }

        if (BGMvolume > BGMsource.volume)
        {
            BGMsource.volume += 0.02f;
        }
        if (BGMvolume < BGMsource.volume)
        {
            BGMsource.volume -= 0.5f;
        }

        BGMsource.pitch = BGMpitch;

        if (BGMmodoru)
        {
            BGMvolume = 0;
            mutetime += Time.deltaTime;
            if (mutetime > 0.5f)
            {
                BGMvolume = 1;
                BGMmodoru = false;
            }
        }
    }
}
