using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StepSystem : MonoBehaviour
{
   
}
[Serializable]
public class Eventss
{
    [SerializeField]
    public string DialogText;
    [SerializeField]
    public ActiveInputMode TypesofInput;
    [SerializeField]
    public List<XRGrabInteractable> grabInteractable;
    [SerializeField]
    public Button button;
    [SerializeField]
    public int waitForSeconds = 0;
}

[Serializable]
public enum ActiveInputModee { GrabInput, WaitForSeconds, SkipButton }