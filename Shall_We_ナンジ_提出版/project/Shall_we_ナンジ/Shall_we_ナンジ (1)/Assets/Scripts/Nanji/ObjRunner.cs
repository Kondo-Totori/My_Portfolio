using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRunner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f*Time.timeScale);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }
}
