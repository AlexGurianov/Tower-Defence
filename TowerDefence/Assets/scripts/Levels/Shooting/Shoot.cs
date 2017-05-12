using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject pumpkin;

    public HashSet<int> mobsBeingShot = new HashSet<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ShootAtTarget(ShotInfo shotInfo)
    {
        mobsBeingShot.Add(shotInfo.ID);
        GameObject newPumpkin = (GameObject)Instantiate(pumpkin, new Vector3(transform.position.x, 0.4f, transform.position.z), Quaternion.identity);
        newPumpkin.GetComponent<Launcher>().shotInfo = shotInfo;
        newPumpkin.GetComponent<PushingOut>().shootingTower = this;
    }

    public void TowerWaitForShotEnd(float delay, int ID)
    {
        StartCoroutine(TowerWaitForShotEndCor(delay, ID));
    }


    IEnumerator TowerWaitForShotEndCor(float delay, int ID)
    {
        yield return new WaitForSeconds(delay);
        mobsBeingShot.Remove(ID);
    }
}
