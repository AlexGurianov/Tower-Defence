using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public GameObject PauseMenuPanel;
    public GameObject SaveQuestionPanel;
    public GameObject InfoPanel;

    // Use this for initialization
    void Start () {
        PauseMenuPanel.SetActive(false);
        SaveQuestionPanel.SetActive(false);
        //InfoPanel.SetActive(false);
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

    public void SettingsButtonClicked()
    {
        Time.timeScale = 1;
        SceneInfoCarrier.sceneInfoCarrier.saver_Loader.SaveGame("SettingsTempSavedGame");
        SceneInfoCarrier.sceneInfoCarrier.comingToSettingsFromGame = true;
        SceneManager.LoadScene("Settings");
    }

    public void ExitButtonClicked()
    {
        SaveQuestionPanel.SetActive(true);
    }

    public void SaveButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.saver_Loader.SaveGame("Saved Game");
        PauseMenuPanel.SetActive(false);
        SaveQuestionPanel.SetActive(false);
        Time.timeScale = 1;
        InfoPanel.SetActive(true);
        StartCoroutine(InfoPanel.GetComponent<FadeInOut>().ShowInfo(2f));
    }

    public void SaveQuestionYesClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.saver_Loader.SaveGame("Saved Game");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveQuestionNoClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
