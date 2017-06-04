using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { tree, tomb, wall_up, wall_right, wall_down, wall_left, tower1, tower2, tower3 };

public class DataStorage : MonoBehaviour {

    public const int MAXMOBSPASSED = 20;

    public static DataStorage dataStorage;

    public Dictionary<int, TowerController> towersDictionary = new Dictionary<int, TowerController>();
    public Dictionary<int, MonsterController> monstersDictionary = new Dictionary<int, MonsterController>();

    public int currentMaxTowerID = 0;
    public int currentMaxMonsterID = 0;

    public int coins;

    public int mobsKilled = 0;

    public int mobsPassed = 0;

    public float elapsedTime;
    public float startTime;

    public bool isPlacingTower = false;

    public int WaveNo = 1;
    public int mobsCreated = 0;

    private void Awake()
    {
        if (dataStorage == null)
        {            
            dataStorage = this;
            startTime = Time.time;
            coins = 100;
        }
        else if (dataStorage != this)
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

    public void IncrementMobsPassed()
    {
        mobsPassed++;
        if (mobsPassed == MAXMOBSPASSED)
            GameObject.Find("GameController").GetComponent<GameController>().EndGame();
    }

    public void RemoveMob(int id)
    {
        monstersDictionary.Remove(id);

        if (monstersDictionary.Count <= 0)
        {
            WaveNo++;
            mobsCreated = 0;
            int num = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWavePopulation(WaveNo);
            float energy = GameObject.Find("MonsterWaveController").GetComponent<MonsterWaveController>().GetWaveEnergy(WaveNo);
            GameObject.Find("GameController").GetComponent<GameController>().initWaves(num, energy);
        }        
    }
}
