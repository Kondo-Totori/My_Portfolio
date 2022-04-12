using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyGameMaster : MonoBehaviour {

    public int gameNumber;
    int currentNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
            currentNumber--;
        if (Input.GetKeyDown(KeyCode.X))
            currentNumber++;

        if (currentNumber > gameNumber)
            currentNumber = 1;
        if (currentNumber < 1)
            currentNumber = gameNumber;
	}
}
