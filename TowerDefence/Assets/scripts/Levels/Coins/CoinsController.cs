using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour {

    public Text CoinsText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCoinsForMonster()
    {
        DataStorage.dataStorage.coins += 20;
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        CoinsText.text = DataStorage.dataStorage.coins.ToString();
    }
}
