using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfilesController : MonoBehaviour {

    public GameObject ModelProfileButton;
    public Transform contentPanel;
    public Scrollbar HorScrollbar;
    public GameObject SetNamePanel;
    public InputField nameInputField;

    List<ProfileButton> ProfileButtonsList = new List<ProfileButton>();

    int selectedProfile = -1;

    public Button SwitchButton;
    public Button DeleteButton;
    
    // Use this for initialization
    void Start () {
        SwitchButton.enabled = false;
        DeleteButton.enabled = false;
        SetNamePanel.SetActive(false);
        SetSounds();
        PopulateProfilesList();
        StartCoroutine(SetScrollbar(0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PopulateProfilesList()
    {
        foreach (var item in ProfileButtonsList)
        {
            item.Remove();
        }
        ProfileButtonsList.Clear();
        int i = 0;
        foreach (var profile in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList)
        {            
            GameObject newButton = Instantiate(ModelProfileButton) as GameObject;
            ProfileButton newProfileButton = newButton.GetComponent<ProfileButton>();
            newProfileButton.ProfileName.text = profile.userName;

            if (i == SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo)
                newProfileButton.SetProfile();
            
            int j = i++;

            newProfileButton.button.onClick.AddListener(() => ProfilesItemClickedFunc(j, true));

            //newButton.transform.SetParent(contentPanel, false);
            newButton.transform.SetParent(contentPanel);
            ProfileButtonsList.Add(newProfileButton);
        }
        StartCoroutine(SetScrollbar(0));
    }

    private void ProfilesItemClickedFunc(int num, bool clicked)
    {
        if (selectedProfile != num)
        {
            if (selectedProfile != -1)
                ProfileButtonsList[selectedProfile].Deselect();
            else
                SwitchButton.enabled = true;                
            if (num == SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo)
                DeleteButton.enabled = false;
            else
                DeleteButton.enabled = true;
            selectedProfile = num;
            ProfileButtonsList[selectedProfile].Select();
        }
    }

    public void NewProfileButtonClicked()
    {
        SetNamePanel.SetActive(true);
    }

    public void OKButtonClicked()
    {
        Profile newProfile = new Profile();
        newProfile.userName = nameInputField.text;
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList.Add(newProfile);
        PopulateProfilesList();
        StartCoroutine(SetScrollbar(1));
        ProfileButtonsList[ProfileButtonsList.Count-1].Select();
        selectedProfile = ProfileButtonsList.Count - 1;
        SwitchButton.enabled = true;
        DeleteButton.enabled = true;
        SetNamePanel.SetActive(false);
    }

    public void CancelButtonClicked()
    {
        SetNamePanel.SetActive(false);
    }

    public void SwitchToProfileButtonClicked()
    {
        ProfileButtonsList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].UnsetProfile();
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo = selectedProfile;
        ProfileButtonsList[selectedProfile].SetProfile();
        DeleteButton.enabled = false;
    }

    public void DeleteProfileButtonClicked()
    {
        ProfileButtonsList[selectedProfile].Remove();
        ProfileButtonsList.RemoveAt(selectedProfile);
        for (int i = selectedProfile; i < ProfileButtonsList.Count; i++)
        {
            ProfileButtonsList[i].button.onClick.RemoveAllListeners();
            int j = i;
            ProfileButtonsList[i].button.onClick.AddListener(() => ProfilesItemClickedFunc(j, true));
        }
        if (selectedProfile < SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo)
            SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo--;
        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList.Remove(SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[selectedProfile]);
        SwitchButton.enabled = false;
        DeleteButton.enabled = false;
        selectedProfile = -1;
    }

    public void BackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator SetScrollbar(float val)
    {
        yield return null;
        HorScrollbar.value = val;
    }

    public void SetSounds()
    {
        GameObject.Find("Button Audio Source").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
}
