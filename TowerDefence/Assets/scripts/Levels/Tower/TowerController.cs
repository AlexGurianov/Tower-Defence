using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public GameObject IndicatorQuad;

    public int ID;

    public bool placed = false;

    float noDeleteTime = 1;

    float placedTime;

    public TowerType type;
    public bool preset = false;

    public int level = 0;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void placeTower()
    {
        Destroy(IndicatorQuad);
        ID = DataStorage.dataStorage.currentMaxTowerID++;
        DataStorage.dataStorage.towersDictionary.Add(ID, GetComponent<TowerController>());
        gameObject.layer = 9;
        placed = true;
        placedTime = Time.time;
        Destroy(GetComponent<DragDrop>());
        GameObject gameController = GameObject.Find("GameController");
        if (!preset)
            GameObject.Find("Coins Text").GetComponent<CoinsController>().SubtractCoinsForTower(type);
        DataStorage.dataStorage.isPlacingTower = false;
        if (gameController != null)
            StartCoroutine(gameController.GetComponent<GameController>().UpdateMonstersPaths());
    }

    private void OnMouseDown()
    {
        if (placed && Time.time - placedTime > noDeleteTime)
        {
            StartCoroutine(FindObjectOfType<SceneButtonsController>().SelectTower(ID));
        }
    }

    public void DeleteTower()
    {
        GameObject.Find("Coins Text").GetComponent<CoinsController>().RefundCoinsForTower(type);
        DataStorage.dataStorage.towersDictionary.Remove(ID);
        Destroy(gameObject);
    }

    public TowerInfo GiveSaveInfo()
    {
        Vector3 pos = transform.position;
        return new TowerInfo(pos.x, pos.y, pos.z, type, preset, level);
    }
}
