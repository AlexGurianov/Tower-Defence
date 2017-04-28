using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Saver_Loader;
    public GameObject monster;
    public GameObject tower1;

	// Use this for initialization
	void Start () {
        if (SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame)
        {
            SavedData savedData = Saver_Loader.GetComponent<Saver_Loader>().Load();
            if (savedData != null)
            {
                foreach (MonsterInfo monsterInfo in savedData.monsters)
                {
                    GameObject monster_obj = CreateMonster(new Vector3(monsterInfo.posx, monsterInfo.posy, monsterInfo.posz));
                    monster_obj.GetComponent<MonsterController>().energy = monsterInfo.energy;
                }
                foreach (TowerInfo towerInfo in savedData.towers)
                {
                    GameObject tower_obj = CreateTower(new Vector3(towerInfo.posx, towerInfo.posy, towerInfo.posz));
                }
            }
        }
        else
        {
            StartCoroutine(CreateMonsterWave(3, 20, new Vector3(0f, 0f, 0f)));
            StartCoroutine(CreateMonsterWave(7, 10, new Vector3(5f, 0f, 8f)));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CreateMonsterWave(float delta, int n, Vector3 pos)
    {
        for (int i = 0; i < n; i++)
        {
            CreateMonster(pos);
            yield return new WaitForSeconds(delta);
        }
    }

    public GameObject CreateMonster(Vector3 pos)
    {
        GameObject monst = Instantiate(monster, pos, Quaternion.identity) as GameObject;
        int ID = DataStorage.dataStorage.currentMaxMonsterID++;
        monst.GetComponent<MonsterController>().ID = ID;
        DataStorage.dataStorage.monstersDictionary.Add(ID, monst.GetComponent<MonsterController>());
        return monst;
    }

    public GameObject CreateTower(Vector3 pos)
    {
        GameObject tower = (GameObject)Instantiate(tower1, pos, Quaternion.Euler(-90, 0, 0));
        tower.GetComponent<TowerController>().placeTower();
        return tower;
    }
}
