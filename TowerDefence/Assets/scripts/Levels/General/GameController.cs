using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject monster;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tree;
    public GameObject tomb;
    public GameObject wall;

    public Text ProfileName;

    // Use this for initialization
    void Start () {
        if (SceneInfoCarrier.sceneInfoCarrier.OpenSavedGame)
        {
            SavedGame savedGame = SceneInfoCarrier.sceneInfoCarrier.saver_Loader.LoadGame(SceneInfoCarrier.sceneInfoCarrier.GameName);
            if (savedGame != null)
            {
                foreach (MonsterInfo monsterInfo in savedGame.monsters)
                {
                    GameObject monster_obj = CreateMonster(new Vector3(monsterInfo.posx, monsterInfo.posy, monsterInfo.posz), monsterInfo.maxEnergy, monsterInfo.energy);
                    monster_obj.GetComponent<MonsterController>().energy = monsterInfo.energy;                    
                }
                CreateSavedTowers(savedGame.towers);
                DataStorage.dataStorage.coins = savedGame.coins;
                DataStorage.dataStorage.elapsedTime = savedGame.elapsedTime;
                DataStorage.dataStorage.mobsKilled = savedGame.mobsKilled;
                DataStorage.dataStorage.mobsPassed = savedGame.mobsPassed;
                DataStorage.dataStorage.WaveNo = savedGame.waveNo;
                DataStorage.dataStorage.mobsCreated = savedGame.mobsCreated;

                int num = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWavePopulation(savedGame.waveNo) - savedGame.mobsCreated;
                float energy = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWaveEnergy(savedGame.waveNo);
                initWaves(num, energy);
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
            int num = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWavePopulation(1);
            float energy = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWaveEnergy(1);
            initWaves(num, energy);
        }
        ProfileName.text = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].userName;
        GameObject.Find("Coins Text").GetComponent<CoinsController>().UpdateCoinsText();
        GameObject.Find("MobsPassed Text").GetComponent<MobsPassedController>().UpdateMobsPassed();
        SetSounds();
	}

    public void initWaves(int num, float waveEnergy)
    {
        //if (GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWavePopulation(DataStorage.dataStorage.WaveNo))
        StartCoroutine(CreateMonsterWave(3, num, new Vector3(40f, 0f, -25f), waveEnergy));
        //StartCoroutine(CreateMonsterWave(7, 10, new Vector3(5f, 0f, 8f), waveEnergy));
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
            tower.GetComponent<TowerController>().preset = towerInfo.preset;
            switch (towerInfo.towerType)
            {
                case TowerType.tower1: case TowerType.tower2: case TowerType.tower3:
                    tower.GetComponent<TowerController>().level = towerInfo.level;
                    tower.GetComponent<ShootingTowerController>().towerAtLevel =
                        GameObject.Find("UpgradeController").GetComponent<UpgradeController>().towersLevelInfo[towerInfo.towerType][towerInfo.level - 1];
                    break;
            }            
            tower.GetComponent<TowerController>().placeTower();
        }
    }

    public IEnumerator CreateMonsterWave(float delta, int n, Vector3 pos, float maxEnergy)
    {
        for (int i = 0; i < n; i++)
        {
            CreateMonster(pos, maxEnergy, maxEnergy);
            DataStorage.dataStorage.mobsCreated++;
            yield return new WaitForSeconds(delta);
        }
    }

    public GameObject CreateMonster(Vector3 pos, float maxEnergy, float energy)
    {
        GameObject monst = Instantiate(monster, pos, Quaternion.identity) as GameObject;
        int ID = DataStorage.dataStorage.currentMaxMonsterID++;
        monst.GetComponent<MonsterController>().ID = ID;
        monst.GetComponent<MonsterController>().maxEnergy = maxEnergy;
        monst.GetComponent<MonsterController>().energy = energy;
        monst.GetComponent<MonsterController>().healthSlider.value = energy / maxEnergy;
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

    public void EndGame()
    {
        GameObject.Find("GameOverController").GetComponent<GameOverController>().CallGameOverPanel();
    }
}
