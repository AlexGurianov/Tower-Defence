using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject ExitConfirmationPanel;
    public Text ProfileName;

	// Use this for initialization
	void Start () {
        ExitConfirmationPanel.SetActive(false);
        ProfileName.text = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].userName;
        SetSounds();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = false;
        //SceneManager.LoadScene("Level1Test");
        SceneManager.LoadScene("MapChoice");
    }

    public void SavedGameButtonClicked()
    {
        if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary.Count > 0)
        {
            SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = true;
            SceneInfoCarrier.sceneInfoCarrier.GameName = "Saved Game";
            if (Application.platform == RuntimePlatform.Android)
                SceneManager.LoadScene("Loading");
            else
            {
                if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[SceneInfoCarrier.sceneInfoCarrier.GameName].isSceneDefault)
                    SceneManager.LoadScene("Level1Test");
                else
                    SceneManager.LoadScene("Level");
            }
        }
    }

    public void NewMapButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.OpenNewMap = true;
        if (Application.platform == RuntimePlatform.Android)
        {            
            SceneManager.LoadScene("Loading");
        }
        else
            SceneManager.LoadScene("NewMap");
    }

    public void HallOFFameButtonClicked()
    {
        SceneManager.LoadScene("HallOfFame");
    }

    public void SettingsButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.comingToSettingsFromGame = false;
        SceneManager.LoadScene("Settings");
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

    public void ManageProfilesButtonClicked()
    {
        SceneManager.LoadScene("Profiles");
    }

    public void SetSounds()
    {
        GameObject.Find("Canvas").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel;
        GameObject.Find("Button Audio Source").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
}
