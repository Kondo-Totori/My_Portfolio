using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    bool editNumber;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("TestScene");
        Destroy(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(this);
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Destroy(this);
            SceneManager.LoadScene("TestScene");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)&&!editNumber)
        {
            editNumber = true;
            UIHolder.playerID = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && editNumber)
            editNumber = false;
        if (editNumber)
        {
            Debug.Log(UIHolder.playerID);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 6;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 7;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 8;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                UIHolder.playerID *= 10;
                UIHolder.playerID += 9;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                UIHolder.playerID *= 10;
            }
        }
    }
}
