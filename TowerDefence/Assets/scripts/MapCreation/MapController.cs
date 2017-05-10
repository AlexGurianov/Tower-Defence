using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetSounds();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSounds()
    {
        GameObject.Find("UI Canvas").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel;
        GameObject.Find("Button Audio Source").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
}
