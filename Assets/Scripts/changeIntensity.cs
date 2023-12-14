using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Unity.VisualScripting;

using UnityEngine;

public class changeIntensity : MonoBehaviour
{
    GameObject[] list;
    private Material plasma;
    private bool enables = true;
    private float initialIntensity = 10;
    public Color colorHot;
    private StepSystemAuto stepSystemAutoScript;
    int indexFromStepScript,track=0;
    public GameObject dialog;
    

    //1 . 10 -> 1 2 3 4 ... 10// lerping

    private float currentIntensity;

    public static changeIntensity instance;
    public static int instanceNumber;

    // Start is called before the first frame update
    public void Start()
    {
        if (instance != this)
        {
            instanceNumber++;
        }
        instance = this;

        gameObject.name = "Slug_"+instanceNumber.ToString();

        //Color.Lerp(colorHot, colorCold, time);
        plasma = gameObject.GetComponent<MeshRenderer>().material;
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
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject stepSysteemAutoObj = GameObject.Find("Dialog");
        StepSystemAuto stepSystemAutoScript = stepSysteemAutoObj.GetComponent<StepSystemAuto>();
        int indexFromStepScript = stepSystemAutoScript.index;
        
        
        string metal1 = stepSystemAutoScript.container[indexFromStepScript].Metal1.name;
        var metal2 = stepSystemAutoScript.container[indexFromStepScript].Metal2.name;

        if (other.name == metal1)
        {
            track++;
        }
        else if (other.name == metal2)
        {
            track++;
        }
        else return;
        if (track == 2)
        {
            stepSystemAutoScript.CountSlugsFunction();
            
        }
    }


}