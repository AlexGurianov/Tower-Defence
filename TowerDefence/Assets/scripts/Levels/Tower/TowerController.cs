using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public GameObject IndicatorQuad;

    public int ID;

    public float ShootingRange = 10;

    private LayerMask EnemiesMask = 1 << 10;

    bool placed = false;

    float reloadTime = 1f;
    float noDeleteTime = 1;

    float lastShotTime;
    float placedTime;

	// Use this for initialization
	void Start () {
        lastShotTime = -reloadTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (placed && Physics.CheckSphere(transform.position, ShootingRange, EnemiesMask))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ShootingRange, EnemiesMask);
            if (Time.time - lastShotTime > reloadTime)
            {
                int targetNum = ChooseTarget(hitColliders);
                if (targetNum != -1)
                {
                    ShotInfo spot = ChooseSpot(hitColliders[targetNum]);
                    //Debug.Log(spot.x);
                    //Debug.Log(spot.z);
                    Debug.DrawRay(spot.targetPosition, Vector3.up * 30, Color.white, 5, false);
                    GetComponent<Shoot>().ShootAtTarget(spot);
                    lastShotTime = Time.time;
                }
            }
        }
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
    }
    
    private int ChooseTarget(Collider[] hitColliders)
    {
        int targetNum = 0;
        for (int i = 1; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<MonsterController>().energy >= 0.05 && hitColliders[i].GetComponent<MonsterController>().CreationTime < hitColliders[targetNum].GetComponent<MonsterController>().CreationTime)
                targetNum = i;
        }
        if (hitColliders[targetNum].GetComponent<MonsterController>().energy < 0.05)
            return -1;
        else
            return targetNum;
    }

    private ShotInfo ChooseSpot(Collider target)
    {
        ShotInfo shotInfo = new ShotInfo(target.transform.position, target.transform.TransformDirection(Vector3.forward), target.GetComponent<Unit>().speed);
        return shotInfo;
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
        DataStorage.dataStorage.towersDictionary.Remove(ID);
        Destroy(gameObject);
    }

    public TowerInfo GiveSaveInfo()
    {
        Vector3 pos = transform.position;
        return new TowerInfo(pos.x, pos.y, pos.z);
    }
}
