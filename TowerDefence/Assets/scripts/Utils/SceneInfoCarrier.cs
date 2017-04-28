using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoCarrier : MonoBehaviour {

    public static SceneInfoCarrier sceneInfoCarrier;

    public float AudioVolume = 1;
    public string User = "Default";

    public bool OpenSavedGame = false;

    void Awake()
    {
        if (sceneInfoCarrier == null)
        {
            DontDestroyOnLoad(gameObject);
            sceneInfoCarrier = this;
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
