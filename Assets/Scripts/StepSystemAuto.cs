using JetBrains.Annotations;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StepSystemAuto : MonoBehaviour
{


    [SerializeField]
    public containers[] container;
    public int index = -1;
    int time = 0, dialogTextIndex = 0,SlugCount=0;
    bool  runCountfunction=false;
    float tracktime;
    public float textSpeed, waitforIncrementIndex = 0f;
    public TextMeshProUGUI TextMeshproText;
    public GameObject testmetal;
    public GameObject tip;



    private void Start()
    {
        printDialog();

    }

    void printDialog()
    {
        StopAllCoroutines();

        index++;
        if (index >= container.Length)
        {
            return;
        }
        StartCoroutine(textLine());
    }


    IEnumerator textLine()
    {
        switch (container[index].TypesofInput)
        {
            case ActiveInputMode.WaitForSeconds:
                time = container[index].waitForSeconds;
                StartCoroutine(waitForSecondsCoroutine(time));
                break;
            case ActiveInputMode.SkipButton:
                container[index].SkipButton.onClick.AddListener(skipButtonFn);
                break;
            case ActiveInputMode.GrabInput:
                container[index].grabInteractable.selectEntered.AddListener(itemPicked);
                break;
            case ActiveInputMode.WeldTwoMetals:
                runCountfunction = true;
                tip.SetActive(true);
                var TipKaSlugComponent1 = tip.GetComponent<slugs>();
                TipKaSlugComponent1.enabled = true;

                break;
        }
        TextMeshproText.text = string.Empty;
        dialogTextIndex = 0;
        while (dialogTextIndex < container[index].DialogText.Length)
        {
            tracktime += Time.deltaTime;
            if (tracktime > textSpeed)
            {
                TextMeshproText.text += container[index].DialogText[dialogTextIndex];
                dialogTextIndex++;
                tracktime = 0;
            }
            yield return new WaitForFixedUpdate();
        }

    }
 

    IEnumerator waitForSecondsCoroutine(int time)
    {
        yield return new WaitForSeconds(time);
        printDialog();

    }
    void skipButtonFn()
    {
        container[index].SkipButton.onClick.RemoveListener(skipButtonFn);
        printDialog();


    }

    [ContextMenu("Debug")]
    void itemPickedDebugWrapper()
    {
        container[index].grabInteractable.selectEntered.RemoveListener(itemPicked);
        printDialog();
    }

    void itemPicked(SelectEnterEventArgs farman)
    {
        container[index].grabInteractable.selectEntered.RemoveListener(itemPicked);
        printDialog();

    }
    IEnumerator slugCountCoroutine()
    {
        yield return null;
    }

    public void CountSlugsFunction()
    {
        if (runCountfunction)
        {
            SlugCount++;
        }
        if (SlugCount >= 85) 
        {
            Debug.Log("pressed space");
            SlugCount = 0;
            //off
            runCountfunction = false;
            var TipKaSlugComponent = tip.GetComponent<slugs>();
            TipKaSlugComponent.offParticles();
            tip.SetActive(false);
            printDialog();
            var listOfSlugs = GameObject.FindGameObjectsWithTag("Slugs");
            Debug.Log(listOfSlugs.Length);


        }
    }
}
[Serializable]
public class containers
{
    [SerializeField]
    public string DialogText;
    [SerializeField]
    public ActiveInputMode TypesofInput;
    [SerializeField]
    public XRGrabInteractable grabInteractable;
    [SerializeField]
    public Button SkipButton;
    [SerializeField]
    public int waitForSeconds = 0;
    public GameObject Metal1, Metal2;
    

}

[Serializable]
public enum ActiveInputMode { GrabInput, WaitForSeconds, SkipButton, WeldTwoMetals }