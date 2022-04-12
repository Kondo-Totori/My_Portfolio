using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    public GameObject shadowsource;
    GameObject source;
    public Moyaeditor editor;
    public bool is_nagaihari;
    float t;
    UIHolder UI;
    // Use this for initialization
    void Start () {
        UI = GameObject.Find("UIHolder").GetComponent<UIHolder>();
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t > 0.03f)
        {
            t = 0;
            source = Instantiate(shadowsource, transform.position, transform.rotation);
            //source.transform.localScale = new Vector2(1.475557f, 1.475557f);
            if (UI.is_vsmode)
            {
                if (is_nagaihari)
                    source.GetComponent<ShadowSource>().layer = 10;
                else
                    source.GetComponent<ShadowSource>().layer = 11;
            }
            source.transform.parent = GameObject.Find("Harizanzou").transform;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Moya(Clone)"&&collision.GetComponent<Moya>().is_jiatteru&&is_nagaihari)
        {
            collision.GetComponent<Moya>().hp--;
            if (collision.GetComponent<Moya>().hp == 0)
            {
                Destroy(collision.gameObject);
                MoyaAudioManager.MoyaSE = true;
                editor.moyakazu--;
            }
        }
    }
}
