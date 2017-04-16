using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingOut : MonoBehaviour {

    //public GameObject Pumpkin;

    //BoxCollider boxcollider;

    const int pushSteps = 40;
    const float waitTime = 0.01f;

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
        GetComponent<Launcher>().Launch();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
