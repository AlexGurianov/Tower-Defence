using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject monster;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tree;
    public GameObject tomb;
    public GameObject wall;

    // Use this for initialization
    void Start () {
        if (SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame)
        {
            SavedGame savedGame = SceneInfoCarrier.sceneInfoCarrier.saver_Loader.LoadGame(SceneInfoCarrier.sceneInfoCarrier.GameName);
            if (savedGame != null)
            {
                foreach (MonsterInfo monsterInfo in savedGame.monsters)
                {
                    GameObject monster_obj = CreateMonster(new Vector3(monsterInfo.posx, monsterInfo.posy, monsterInfo.posz));
                    monster_obj.GetComponent<MonsterController>().energy = monsterInfo.energy;                    
                }
                CreateSavedTowers(savedGame.towers);
                StartCoroutine(CreateMonsterWave(3, 20, new Vector3(0f, 0f, 0f)));
                StartCoroutine(CreateMonsterWave(7, 10, new Vector3(5f, 0f, 8f)));
            }
            else
            {
                Debug.Log("No such saved game");
            }
        }
        else
        {
            if (SceneInfoCarrier.sceneInfoCarrier.GameName != "Default")
            {
                SavedMap savedMap = SceneInfoCarrier.sceneInfoCarrier.saver_Loader.LoadMap(SceneInfoCarrier.sceneInfoCarrier.GameName);
                if (savedMap != null)
                {
                    CreateSavedTowers(savedMap.towers);
                }
            }
            StartCoroutine(CreateMonsterWave(3, 20, new Vector3(0f, 0f, 0f)));
            StartCoroutine(CreateMonsterWave(7, 10, new Vector3(5f, 0f, 8f)));
        }
        SetSounds();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateSavedTowers(List<TowerInfo> towers)
    {
        foreach (TowerInfo towerInfo in towers)
        {
            GameObject prefab;
            Quaternion rotation;
            switch (towerInfo.towerType)
            {
                case TowerType.tower1:
                default:
                    prefab = tower1;
                    rotation = Quaternion.Euler(-90, 0, 0);
                    break;
                case TowerType.tower2:
                    prefab = tower2;
                    rotation = Quaternion.Euler(-90, 0, 0);
                    break;
                case TowerType.tower3:
                    prefab = tower3;
                    rotation = Quaternion.Euler(-90, 0, 0);
                    break;
                case TowerType.tree:
                    prefab = tree;
                    rotation = Quaternion.Euler(-90, 0, 0);
                    break;
                case TowerType.tomb:
                    prefab = tomb;
                    rotation = Quaternion.Euler(-90, 0, 0);
                    break;
                case TowerType.wall_up:
                    prefab = wall;
                    rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case TowerType.wall_right:
                    prefab = wall;
                    rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case TowerType.wall_down:
                    prefab = wall;
                    rotation = Quaternion.Euler(0, 270, 0);
                    break;
                case TowerType.wall_left:
                    prefab = wall;
                    rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
            GameObject tower = (GameObject)Instantiate(prefab, new Vector3(towerInfo.posx, towerInfo.posy, towerInfo.posz), rotation);
            tower.GetComponent<TowerController>().type = towerInfo.towerType;
            tower.GetComponent<TowerController>().placeTower();
        }
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

    /*public GameObject CreateTower(Vector3 pos, Quaternion rotation)
    {
        GameObject tower = (GameObject)Instantiate(tower1, pos, Quaternion.Euler(-90, 0, 0));
        tower.GetComponent<TowerController>().placeTower();
        return tower;
    }*/

    public IEnumerator UpdateMonstersPaths()
    {
        yield return null;
        GameObject.Find("A* Algorithm").GetComponent<Pathfinding>().UpdateGrid();
        foreach (MonsterController monsterController in DataStorage.dataStorage.monstersDictionary.Values)
        {
            StartCoroutine(monsterController.gameObject.GetComponent<Unit>().UpdatePath());
        }
    }

    public void SetSounds()
    {
        GameObject.Find("UI Canvas").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.musicLevel;
        GameObject.Find("Button Audio Source").GetComponent<AudioSource>().volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.navigationSoundLevel;
    }
}
