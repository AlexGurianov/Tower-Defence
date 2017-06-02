using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    public GameObject GameOverPanel;
    public Button OKButton;
    public Text timeText;
    public Text killedMobsText;

    // Use this for initialization
    void Start () {
        GameOverPanel.SetActive(false);
        //OKButton.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        DataStorage.dataStorage.elapsedTime += Time.time - DataStorage.dataStorage.startTime;
        timeText.text = appendZeroes(Mathf.FloorToInt(DataStorage.dataStorage.elapsedTime / 60)) +
            ":" + appendZeroes(Mathf.FloorToInt(DataStorage.dataStorage.elapsedTime - 60 * Mathf.Floor(DataStorage.dataStorage.elapsedTime / 60)));
        killedMobsText.text = DataStorage.dataStorage.mobsKilled.ToString();
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedResultsList.Add(new SavedResult(DataStorage.dataStorage.elapsedTime, DataStorage.dataStorage.mobsKilled));
        //StartCoroutine(DisplayKilledMobsNum(DataStorage.dataStorage.mobsKilled));
    }

    string appendZeroes(int number)
    {
        if (number < 10)
            return "0" + number.ToString();
        else
            return number.ToString();
    }

    /*
    IEnumerator DisplayKilledMobsNum(int mobsKilled)
    {
        for (int i = 0; i < mobsKilled; i++)
        {
            killedMobsText.text = i.ToString();
            yield return StartCoroutine(WaitForRealTime(2f));
        }
        killedMobsText.text = mobsKilled.ToString();
        OKButton.enabled = true;
    }

    public static IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }
    }*/

    public void OKButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
