using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject ExitConfirmationPanel;

	// Use this for initialization
	void Start () {
        ExitConfirmationPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = false;
        SceneManager.LoadScene("Level1Test");
    }

    public void SavedGameButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = true;
        SceneManager.LoadScene("Level1Test");
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
