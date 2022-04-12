using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] BGMs, SEs;
    [SerializeField]
    AudioSource BGM,SE_1,SE_2;

    public static bool f_BGM, d_BGM, f_confirm, f_point, f_combo, f_set, f_shuffle, f_result, d_confirm, d_correct, d_incorrect, d_result,f_incorrect;
    private bool fadeout; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout)
        {
            if (BGM.volume > 0)
            {
                Debug.Log("aaa");
                BGM.volume -= 0.02f;
            }
        }

        if (f_BGM)
        {
            f_BGM = false;
            StartCoroutine(BGMChange(BGMs[0]));
        }

        if (d_BGM)
        {
            d_BGM = false;
            StartCoroutine(BGMChange(BGMs[1]));
        }

        if (f_confirm)
        {
            f_confirm = false;
            SE_1.PlayOneShot(SEs[0]);
        }

        if (f_point)
        {
            f_point = false;
            SE_1.PlayOneShot(SEs[1]);
        }

        if (f_combo)
        {
            f_combo = false;
            SE_2.PlayOneShot(SEs[2]);
        }

        if (f_set)
        {
            f_set = false;
            SE_2.PlayOneShot(SEs[3]);
        }

        if (f_shuffle)
        {
            f_shuffle = false;
            SE_1.PlayOneShot(SEs[4]);
        }

        if (f_result)
        {
            f_result = false;
            SE_1.PlayOneShot(SEs[5]);
        }

        if (d_confirm)
        {
            d_confirm = false;
            SE_1.PlayOneShot(SEs[6]);
        }

        if (d_correct)
        {
            d_correct = false;
            SE_1.PlayOneShot(SEs[7]);
        }

        if (d_incorrect)
        {
            d_incorrect = false;
            SE_1.PlayOneShot(SEs[8]);
        }

        if (d_result)
        {
            d_result = false;
            SE_1.PlayOneShot(SEs[9]);
        }
        if (f_incorrect)
        {
            f_incorrect = false;
            SE_1.PlayOneShot(SEs[10]);
        }

    }

    IEnumerator BGMChange(AudioClip a)
    {
        fadeout = true;
        yield return new WaitForSeconds(1);
        BGM.Stop();
        fadeout = false;
        BGM.clip = a;
        BGM.volume = 1;
        BGM.Play();
    }
}
