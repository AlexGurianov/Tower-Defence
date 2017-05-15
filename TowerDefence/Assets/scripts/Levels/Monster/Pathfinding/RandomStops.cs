using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStops {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static float TimeToStop(float lambda)
    {
        return -Mathf.Log(Random.Range(0.1f, 1))/lambda;
    }

    public static float TimeOfStop()
    {
        return Random.Range(0.5f, 1.5f);
    }
}