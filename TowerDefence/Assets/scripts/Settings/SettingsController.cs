using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {

    public Slider musicLevel;
    public Slider gameSoundLevel;
    public Slider navigationSoundLevel;

	// Use this for initialization
	void Start () {
        musicLevel.value = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel;
        gameSoundLevel.value = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.gameSoundLevel;
        navigationSoundLevel.value = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel = musicLevel.value;
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.gameSoundLevel = gameSoundLevel.value;
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel = navigationSoundLevel.value;
        if (SceneInfoCarrier.sceneInfoCarrier.comingToSettingsFromGame)
        {
            SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = true;
            SceneInfoCarrier.sceneInfoCarrier.GameName = "SettingsTempSavedGame";
            if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[SceneInfoCarrier.sceneInfoCarrier.GameName].isSceneDefault)
                SceneManager.LoadScene("Level1Test");
            else
                SceneManager.LoadScene("Level");
        }
        else
            SceneManager.LoadScene("MainMenu");
    }
}
