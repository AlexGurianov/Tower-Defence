using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { tree, tomb, wall_up, wall_right, wall_down, wall_left, tower1 };

public class DataStorage : MonoBehaviour {

    public static DataStorage dataStorage;

    public Dictionary<int, TowerController> towersDictionary = new Dictionary<int, TowerController>();
    public Dictionary<int, MonsterController> monstersDictionary = new Dictionary<int, MonsterController>();

    public int currentMaxTowerID = 0;
    public int currentMaxMonsterID = 0;

    private void Awake()
    {
        if (dataStorage == null)
        {            
            dataStorage = this;         
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

}
