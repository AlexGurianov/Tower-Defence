using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HallOfFameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetSounds();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetSounds()
    {
        GameObject.Find("Canvas").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel;
        GameObject.Find("Button Audio Source").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
}
