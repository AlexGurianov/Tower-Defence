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

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedGame.dat");

        SavedData savedData = new SavedData();

        foreach (int key in DataStorage.dataStorage.towersDictionary.Keys)
            savedData.towers.Add(DataStorage.dataStorage.towersDictionary[key].GiveSaveInfo());
        foreach (int key in DataStorage.dataStorage.monstersDictionary.Keys)
            savedData.monsters.Add(DataStorage.dataStorage.monstersDictionary[key].GiveSaveInfo());

        bf.Serialize(file, savedData);
        file.Close();
    }

    public SavedData Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedGame.dat", FileMode.Open);

            SavedData savedData = (SavedData)bf.Deserialize(file);

            file.Close();

            return savedData;
        }
        return null;
    }

}
