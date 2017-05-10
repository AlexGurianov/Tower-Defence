using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoCarrier : MonoBehaviour {

    public Saver_Loader saver_Loader;

    public GameInfo gameInfo;

    public static SceneInfoCarrier sceneInfoCarrier;

    public bool OpenSavedGame = false;
    public string GameName = "";

    public bool comingToSettingsFromGame = false;

    void Awake()
    {
        if (sceneInfoCarrier == null)
        {
            saver_Loader = GetComponent<Saver_Loader>();
            DontDestroyOnLoad(gameObject);
            sceneInfoCarrier = this;
            gameInfo = saver_Loader.LoadAll();
            if (gameInfo == null)
            {
                gameInfo = new GameInfo();
                Profile defaultProfile = new Profile();
                defaultProfile.userName = "User";
                gameInfo.profilesList.Add(defaultProfile);
            }
        }
        else if (sceneInfoCarrier != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
