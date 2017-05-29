using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Saver_Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Saver_Loader saver_Loader;

    public void SaveAll()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedInfo.dat");

        bf.Serialize(file, SceneInfoCarrier.sceneInfoCarrier.gameInfo);
        file.Close();
    }

    public GameInfo LoadAll()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedInfo.dat", FileMode.Open);

            GameInfo gameInfo = (GameInfo)bf.Deserialize(file);

            file.Close();

            return gameInfo;
        }
        return null;
    }

    public void SaveGame(string gameName)
    {
        SavedGame savedGame = new SavedGame();

        foreach (int key in DataStorage.dataStorage.towersDictionary.Keys)
            savedGame.towers.Add(DataStorage.dataStorage.towersDictionary[key].GiveSaveInfo());
        foreach (int key in DataStorage.dataStorage.monstersDictionary.Keys)
            savedGame.monsters.Add(DataStorage.dataStorage.monstersDictionary[key].GiveSaveInfo());

        if ((SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame &&
            SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[SceneInfoCarrier.sceneInfoCarrier.GameName].isSceneDefault) ||
            !SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame && SceneInfoCarrier.sceneInfoCarrier.GameName == "Default")
            savedGame.isSceneDefault = true;
        else
            savedGame.isSceneDefault = false;

        savedGame.coins = DataStorage.dataStorage.coins;
        savedGame.elapsedTime = DataStorage.dataStorage.elapsedTime;
        savedGame.mobsKilled = 0;

        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[gameName] = savedGame;
    }

    public SavedGame LoadGame(string gameName)
    {
        if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[gameName] != null)
            return SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedGamesDictionary[gameName];
        else
            return null;
    }

    public void SaveMap(string mapName)
    {
        SavedMap savedMap = new SavedMap();

        foreach (int key in DataStorage.dataStorage.towersDictionary.Keys)
            savedMap.towers.Add(DataStorage.dataStorage.towersDictionary[key].GiveSaveInfo());

        SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary[mapName] = savedMap;
    }

    public SavedMap LoadMap(string mapName)
    {
        if (SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary[mapName] != null)
            return SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].savedMapsDictionary[mapName];
        else
            return null;
    }

    public void OnApplicationQuit()
    {
        SaveAll();
    }
}
