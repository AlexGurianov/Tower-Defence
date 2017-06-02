using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobsPassedController : MonoBehaviour {

    public Text MobsPassedText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateMobsPassed()
    {
        MobsPassedText.text = (DataStorage.MAXMOBSPASSED - DataStorage.dataStorage.mobsPassed).ToString();
    }
}
