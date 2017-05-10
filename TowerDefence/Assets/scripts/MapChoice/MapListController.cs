using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MapListController : MonoBehaviour {

    public GameObject mapTile;
    public Transform contentPanel;

	// Use this for initialization
	void Start () {
        foreach (string map_name in SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary.Keys)
        {
            GameObject newButton = Instantiate(mapTile) as GameObject;
            newButton.transform.Find("Map Name").GetComponent<Text>().text = map_name;
            if (File.Exists(Application.persistentDataPath + "/" + map_name + ".png")) {
                byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + map_name + ".png");
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(bytes);
                newButton.transform.Find("Map Image").GetComponent<Image>().sprite = Sprite.Create(texture, new Rect (0, 0, texture.width, texture.height), new Vector2());
            }
            newButton.transform.SetParent(contentPanel, false);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
