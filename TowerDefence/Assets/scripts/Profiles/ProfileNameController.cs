using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class ProfileNameController : MonoBehaviour {

    public Button OKButton;
    public InputField nameInputField;

    // Use this for initialization
    void Start()
    {
        nameInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    string GenerateName()
    {
        int i = 1;
        foreach (Profile profile in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList)
            if (profile.userName.StartsWith("User "))
            {
                string resultString = Regex.Match(profile.userName, @"\d+").Value;
                if (resultString != "" && Int32.Parse(resultString) >= i)
                    i = Int32.Parse(resultString) + 1;
            }
        return "User " + i.ToString();
    }

    bool Check(string text)
    {
        if (text == "")
            return false;
        else foreach (Profile profile in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList)
                if (profile.userName == text)
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