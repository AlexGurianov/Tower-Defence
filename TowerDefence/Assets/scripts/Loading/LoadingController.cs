using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (SceneInfoCarrier.sceneInfoCarrier.OpenNewMap)
        {
            //SceneInfoCarrier.sceneInfoCarrier.OpenNewMap = false;
            SceneManager.LoadScene("NewMap");
        }
        else if (SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame)
        {
            if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[SceneInfoCarrier.sceneInfoCarrier.GameName].isSceneDefault)
                SceneManager.LoadScene("Level1Test");
            else
                SceneManager.LoadScene("Level");
        } else if (SceneInfoCarrier.sceneInfoCarrier.GameName == "Default")
            SceneManager.LoadScene("Level1Test");
        else
            SceneManager.LoadScene("Level");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
