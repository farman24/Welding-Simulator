using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class changeIntensity : MonoBehaviour
{
    private Material plasma;
    private bool enables = true;
    private float initialIntensity = 10;
    public Color colorHot;

    //1 . 10 -> 1 2 3 4 ... 10// lerping

    private float currentIntensity;

    // Start is called before the first frame update
    void Start()
    {
        //Color.Lerp(colorHot, colorCold, time);
        plasma =gameObject.GetComponent<MeshRenderer>().material;
        currentIntensity = initialIntensity;
        plasma.SetColor("_EmissionColor", colorHot * currentIntensity);
        StartCoroutine(dec());
    }

    IEnumerator dec()
    {
        while (enables && currentIntensity > 0)
        {
            currentIntensity -= Time.deltaTime * 2;
            plasma.SetColor("_EmissionColor", colorHot * currentIntensity);
            yield return new WaitForFixedUpdate();
        }
    }

}