using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonsController : MonoBehaviour {

    public PauseMenuController pauseMenuController;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseButtonClicked()
    {
        pauseMenuController.CallPauseMenu();
    }
}
