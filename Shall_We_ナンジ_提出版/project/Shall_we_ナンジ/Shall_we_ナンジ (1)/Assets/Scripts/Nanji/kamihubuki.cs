using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamihubuki : MonoBehaviour
{
    public ParticleSystem hubuki;
    UIHolder UI;

    void Start()
    {
        UI = GameObject.Find("UIHolder").GetComponent<UIHolder>();
    }

    public void hubukihurasu()
    {
        if (UI.score > UI.score2P)
        {
            hubuki.gameObject.layer = 10;
            hubuki.Play();
        }
        else if (UI.score < UI.score2P)
        {
            hubuki.gameObject.layer = 11;
            hubuki.Play();
        }
    }
}
