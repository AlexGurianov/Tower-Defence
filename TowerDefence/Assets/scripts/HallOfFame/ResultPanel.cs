using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    public Text ProfileName;
    public Text MobsKilled;
    public Text TimeAlive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetResultsPanel(ResultPanelInfo resultPanelInfo)
    {
        ProfileName.text = resultPanelInfo.profileName;
        MobsKilled.text = resultPanelInfo.mobsKilled.ToString();
        TimeAlive.text = appendZeroes(Mathf.FloorToInt(resultPanelInfo.timeAlive / 60)) + ":" + appendZeroes(Mathf.FloorToInt(resultPanelInfo.timeAlive - 60 * Mathf.Floor(resultPanelInfo.timeAlive / 60)));
    }

    string appendZeroes(int number)
    {
        if (number < 10)
            return "0" + number.ToString();
        else
            return number.ToString();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
