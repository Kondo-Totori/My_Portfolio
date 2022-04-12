using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    Animation Opening;
    public bool is_opEnd;
    [SerializeField]
    Text clickToPlay;
    private float a;
    [SerializeField]
    Text factory;
    private SceneChanger changer;

    // Start is called before the first frame update
    void Start()
    {
        changer = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        Opening.Play("Opening");
    }

    // Update is called once per frame
    void Update()
    {
        clickToPlay.color = new Color(0, 0, 0, a);
        a = 0.4f*Mathf.Sin(Time.time*3)+0.6f;
    }

    public void CharaAnime(string s)
    {
        factory.text = s;
    }

    public void ToFactory()
    {
        if (is_opEnd)
        {
            BGMManager.f_confirm=true;
            changer.TitleToFactory();
        }
    }

    public void ToPizza()
    {
        if (is_opEnd)
        {
            BGMManager.d_confirm = true;
            changer.TitleToPizza();
        }
    }

}
