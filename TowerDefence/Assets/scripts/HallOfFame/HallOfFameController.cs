using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUserNameComparer : IComparer<ResultPanelInfo>
{
    public int Compare(ResultPanelInfo x, ResultPanelInfo y)
    {
        if (x.profileName.CompareTo(y.profileName) != 0)
            return x.profileName.CompareTo(y.profileName);
        else if (-x.mobsKilled.CompareTo(y.mobsKilled) != 0)
            return -x.mobsKilled.CompareTo(y.mobsKilled);
        else
            return -x.timeAlive.CompareTo(y.timeAlive);
    }
}

public class ResultsMobsKilledComparer : IComparer<ResultPanelInfo>
{
    public int Compare(ResultPanelInfo x, ResultPanelInfo y)
    {
        if (-x.mobsKilled.CompareTo(y.mobsKilled) != 0)
            return -x.mobsKilled.CompareTo(y.mobsKilled);
        else if (-x.timeAlive.CompareTo(y.timeAlive) != 0)
            return -x.timeAlive.CompareTo(y.timeAlive);
        else
            return x.profileName.CompareTo(y.profileName);
    }
}

public class ResultsTimeAliveComparer : IComparer<ResultPanelInfo>
{
    public int Compare(ResultPanelInfo x, ResultPanelInfo y)
    {
        if (-x.timeAlive.CompareTo(y.timeAlive) != 0)
            return -x.timeAlive.CompareTo(y.timeAlive);
        else if (-x.mobsKilled.CompareTo(y.mobsKilled) != 0)
            return -x.mobsKilled.CompareTo(y.mobsKilled);
        else
            return x.profileName.CompareTo(y.profileName);
    }
}

public class HallOfFameController : MonoBehaviour {

    public GameObject ModelResultPanel;
    public Transform contentPanel;

    public Scrollbar VerScrollbar;

    ResultsUserNameComparer resultsUserNameComparer = new ResultsUserNameComparer();
    ResultsMobsKilledComparer resultsMobsKilledComparer = new ResultsMobsKilledComparer();
    ResultsTimeAliveComparer resultsTimeAliveComparer = new ResultsTimeAliveComparer();

    List<ResultPanelInfo> ResultPanelInfosList = new List<ResultPanelInfo>();

    // Use this for initialization
    void Start () {
        
        SetSounds();        
        foreach (var profile in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList)
            foreach (var result in profile.savedResultsList)
            {
                ResultPanelInfosList.Add(new ResultPanelInfo(profile.userName, result.mobsKilled, result.timePlayed));
            }
        PopulateResultsList();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SortByUserNameButtonClicked()
    {
        ResultPanelInfosList.Sort(resultsUserNameComparer);
        PopulateResultsList();
    }

    public void SortByMobsKilledButtonClicked()
    {
        ResultPanelInfosList.Sort(resultsMobsKilledComparer);
        PopulateResultsList();
    }

    public void SortByTimeAliveButtonClicked()
    {
        ResultPanelInfosList.Sort(resultsTimeAliveComparer);
        PopulateResultsList();
    }

    void PopulateResultsList()
    {
        foreach (var resultPanel in contentPanel.GetComponentsInChildren<ResultPanel>())
        {
            resultPanel.Remove();
        }
        foreach (var resultPanelInfo in ResultPanelInfosList)
        {
            GameObject newPanel = Instantiate(ModelResultPanel) as GameObject;
            ResultPanel newResultPanel = newPanel.GetComponent<ResultPanel>();
            newResultPanel.SetResultsPanel(resultPanelInfo);
            newPanel.transform.SetParent(contentPanel);                
        }
        StartCoroutine(SetScrollbar(1));
    }

    IEnumerator SetScrollbar(float val)
    {
        yield return null;
        VerScrollbar.value = val;
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
