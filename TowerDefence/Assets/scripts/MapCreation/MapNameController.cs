using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        return "New Map";
    }

    bool Check(string text)
    {
        if (text != "")
            return true;
        else
            return false;
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
