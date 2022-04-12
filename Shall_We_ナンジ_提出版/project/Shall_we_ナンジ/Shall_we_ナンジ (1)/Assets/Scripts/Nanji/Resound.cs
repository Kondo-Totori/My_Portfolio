using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resound : MonoBehaviour
{
    public bool narasu;
    public AudioSource naru;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (narasu)
        {
            narasu = false;
            naru.Play();
        }
    }

    void narashitai()
    {
        narasu = true;
    }
}
