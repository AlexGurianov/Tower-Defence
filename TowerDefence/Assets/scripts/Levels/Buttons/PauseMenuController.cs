using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public GameObject PauseMenuPanel;

    // Use this for initialization
    void Start () {
        PauseMenuPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallPauseMenu()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnButtonClicked()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
