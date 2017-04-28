using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

    public IEnumerator ShowInfo(float time)
    {
        StartCoroutine(FadeIn(time/2));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeOut(time/2));
    }


    IEnumerator FadeIn(float time)
    {
        for (float f = 0; f <= 1; f += 0.01f)
        {
            GetComponent<CanvasGroup>().alpha = f;
            yield return new WaitForSeconds(time / 100);
        }
    }

    IEnumerator FadeOut(float time)
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            GetComponent<CanvasGroup>().alpha = f;
            yield return new WaitForSeconds(time / 100);
        }
    }
}
