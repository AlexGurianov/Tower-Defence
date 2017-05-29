using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class MapNameController : MonoBehaviour {

    public Button OKButton;
    public InputField nameInputField;

	// Use this for initialization
	void Start () {
		nameInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    string GenerateName()
    {
        int i = 1;
        foreach (string mapName in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary.Keys)
            if (mapName.StartsWith("New Map "))
            {
                string resultString = Regex.Match(mapName, @"\d+").Value;
                if (resultString != "" && Int32.Parse(resultString) >= i)
                    i = Int32.Parse(resultString) + 1;
            }
        return "New Map " + i.ToString();
    }

    bool Check(string text)
    {
        if (text == "")
            return false;
        else foreach (string mapName in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary.Keys)
                if (mapName == text)
                    return false;
        return true;
    }

    void OnEnable()
    {
        nameInputField.text = GenerateName();
    }

    public void ValueChangeCheck()
    {
        if (!Check(nameInputField.text))
            OKButton.interactable = false;
        else if (!OKButton.interactable)
            OKButton.interactable = true;
    }
}
