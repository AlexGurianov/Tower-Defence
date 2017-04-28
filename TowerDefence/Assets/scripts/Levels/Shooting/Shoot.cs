using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject pumpkin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ShootAtTarget(ShotInfo shotInfo)
    {
        GameObject newPumpkin = (GameObject)Instantiate(pumpkin, new Vector3(transform.position.x, 0.4f, transform.position.z), Quaternion.identity);
        newPumpkin.GetComponent<Launcher>().shotInfo = shotInfo;
    }
}
