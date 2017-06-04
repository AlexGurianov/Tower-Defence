using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetWavePopulation(int waveNo)
    {
        return Mathf.RoundToInt(4 * Mathf.Sqrt(waveNo));
    }

    public float GetWaveEnergy(int waveNo)
    {
        return Mathf.Log(Mathf.Exp(1)*waveNo);
    }
}
