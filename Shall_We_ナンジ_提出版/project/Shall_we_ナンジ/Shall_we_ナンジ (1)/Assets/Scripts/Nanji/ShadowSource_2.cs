using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSource_2 : MonoBehaviour
{
    public float a=1,ran;

    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(-0.2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (a > 0)
        {
            a -= 0.06f;
        }
        //transform.localScale = new Vector2(a*0.137f,a * 0.137f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f-(Time.timeScale)/15);
    }
}
