using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public GameObject ExitConfirmationPanel;

	// Use this for initialization
	void Start () {
        ExitConfirmationPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//panel.SetActive(false);
	}

    public void ExitButtonClicked()
    {
        ExitConfirmationPanel.SetActive(true);
    }

    public void QuitYesButtonClicked()
    {
        Application.Quit();
    }

    public void QuitNoButtonClicked()
    {
        ExitConfirmationPanel.SetActive(false);
    }
}
