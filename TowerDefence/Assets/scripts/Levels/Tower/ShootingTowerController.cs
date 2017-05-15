using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowerController : MonoBehaviour {

    public float ShootingRange = 10;
    public float reloadTime = 1f;

    private LayerMask EnemiesMask = 1 << 10;

    float lastShotTime;

    // Use this for initialization
    void Start () {
        lastShotTime = -reloadTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<TowerController>().placed && Physics.CheckSphere(transform.position, ShootingRange, EnemiesMask))
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
                    //Debug.DrawRay(spot.targetPosition, Vector3.up * 30, Color.white, 5, false);
                    GetComponent<Shoot>().ShootAtTarget(spot);
                    lastShotTime = Time.time;
                }
            }
        }
    }

    private int ChooseTarget(Collider[] hitColliders)
    {
        int targetNum = 0;
        for (int i = 1; i < hitColliders.Length; i++)
        {
            if (!GetComponent<Shoot>().mobsBeingShot.Contains(hitColliders[i].GetComponent<MonsterController>().ID)
                && hitColliders[i].GetComponent<MonsterController>().energy >= 0.05
                && hitColliders[i].GetComponent<MonsterController>().CreationTime < hitColliders[targetNum].GetComponent<MonsterController>().CreationTime)
                targetNum = i;
        }
        if (hitColliders[targetNum].GetComponent<MonsterController>().energy < 0.05 || GetComponent<Shoot>().mobsBeingShot.Contains(hitColliders[targetNum].GetComponent<MonsterController>().ID))
            return -1;
        else
            return targetNum;
    }

    private ShotInfo ChooseSpot(Collider target)
    {
        ShotInfo shotInfo = new ShotInfo(target.transform.position, target.transform.TransformDirection(Vector3.forward), target.GetComponent<Unit>().speed, target.GetComponent<MonsterController>().ID);
        return shotInfo;
    }
}
