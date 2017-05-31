using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingOut : MonoBehaviour {

    const float pushTime = 0.2f;

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
        /*
        float delta = pumpkinHeight / pushSteps;
        for (int i = 0; i < pushSteps; i++)
        {
            transform.Translate(0, delta, 0);
            yield return new WaitForSeconds(waitTime);
        }*/
        float time = 0;
        while (time < pushTime)
        {
            transform.Translate(0, pumpkinHeight *(Mathf.Clamp01((time + Time.deltaTime) / pushTime) - Mathf.Clamp01(time / pushTime)), 0);
            time += Time.deltaTime;
            yield return null;            
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
