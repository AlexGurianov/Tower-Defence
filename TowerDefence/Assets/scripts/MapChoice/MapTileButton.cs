using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapTileButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TileButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame = false;
        SceneInfoCarrier.sceneInfoCarrier.GameName = transform.parent.Find("Map Name").GetComponent<Text>().text;
        if (SceneInfoCarrier.sceneInfoCarrier.GameName == "Default")
            SceneManager.LoadScene("Level1Test");
        else
            SceneManager.LoadScene("Level");
    } 
}
