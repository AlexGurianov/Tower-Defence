using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingOut : MonoBehaviour {

    const int pushSteps = 20;
    const float waitTime = 0.01f;

    public Shoot shootingTower;

    const float addedShotDelay = 0.1f;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().useGravity = false;   
        float pumpkinHeight = GetComponent<Collider>().bounds.size.y;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        StartCoroutine(Lift(pumpkinHeight));
    }

    IEnumerator Lift(float pumpkinHeight)
    {  
        float delta = pumpkinHeight / pushSteps;
        for (int i = 0; i < pushSteps; i++)
        {
            transform.Translate(0, delta, 0);
            yield return new WaitForSeconds(waitTime);
        }
        GetComponent<Rigidbody>().useGravity = true;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }
        float t = GetComponent<Launcher>().Launch();
        shootingTower.TowerWaitForShotEnd(t + addedShotDelay, GetComponent<Launcher>().shotInfo.ID);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
