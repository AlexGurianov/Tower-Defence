using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplaySliderValue(float value)
    {
        transform.Find("Value Text").GetComponent<Text>().text = System.Math.Round(value, 2).ToString();
    }
}
