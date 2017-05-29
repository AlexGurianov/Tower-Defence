using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MapButtonsController : MonoBehaviour {

    public GameObject SetNamePanel;
    public GameObject InfoPanel;
    public GameObject SaveQuestionPanel;
    public InputField nameInputField;
    public GameObject UICanvas;

    public bool isExiting = false;

    public bool makeScreenshot = false;

	// Use this for initialization
	void Start () {
        SetNamePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveButtonClicked()
    {
        SetNamePanel.SetActive(true);
    }

    public void YesButtonClicked()
    {
        SaveQuestionPanel.SetActive(false);
        SetNamePanel.SetActive(true);
    }

    public void ExitButtonClicked()
    {
        isExiting = true;
    }

    public void OKButtonClicked()
    {
        SceneInfoCarrier.sceneInfoCarrier.saver_Loader.SaveMap(nameInputField.text);
        SetNamePanel.SetActive(false);
        StartCoroutine(CaptureScreen(SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].userName 
            + "_" + nameInputField.text));
        if (!isExiting)
        {
            nameInputField.text = "";            
            InfoPanel.SetActive(true);
            StartCoroutine(InfoPanel.GetComponent<FadeInOut>().ShowInfo(2f));
        }      
    }

    public void CancelButtonClicked()
    {
        if (isExiting)
            SceneManager.LoadScene("MainMenu");
        nameInputField.text = "";
        SetNamePanel.SetActive(false);
    }

    public IEnumerator CaptureScreen(string name)
    {
        yield return null;
        UICanvas.GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame();
        Application.CaptureScreenshot(System.IO.Path.Combine(Application.persistentDataPath,  name + ".png"));
        //Application.CaptureScreenshot(name + ".png");
        UICanvas.GetComponent<Canvas>().enabled = true;
        if (isExiting)
            SceneManager.LoadScene("MainMenu");
    }
}
