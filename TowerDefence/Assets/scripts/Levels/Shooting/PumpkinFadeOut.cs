using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinFadeOut : MonoBehaviour {

    // Use this for initialization

    public float timeToFade;
    public Material fadeMaterial;
    public GameObject drawnObject;

    // Use this for initialization
    void Start()
    {

    }

    public void FadePumpkinOut()
    {
        drawnObject.GetComponent<SkinnedMeshRenderer>().material = fadeMaterial;
        StartCoroutine(FadeOutCoroutine());
    }


    IEnumerator FadeOutCoroutine()
    {
        Color color = drawnObject.GetComponent<SkinnedMeshRenderer>().material.color;
        while (color.a > 0.05) {
            drawnObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime / timeToFade);
            yield return new WaitForSeconds(timeToFade * Time.deltaTime);
            color = drawnObject.GetComponent<SkinnedMeshRenderer>().material.color;
        }
        Object.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
