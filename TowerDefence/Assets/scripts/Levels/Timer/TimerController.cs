using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Text TimerText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float time = DataStorage.dataStorage.elapsedTime + Time.time - DataStorage.dataStorage.startTime;
        TimerText.text = appendZeroes(Mathf.FloorToInt(time / 60)) + ":" + appendZeroes(Mathf.FloorToInt(time - 60*Mathf.Floor(time / 60)));
    }

    string appendZeroes(int number)
    {
        if (number < 10)
            return "0" + number.ToString();
        else
            return number.ToString();
    }
}
