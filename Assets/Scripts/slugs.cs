using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class slugs : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public Transform gunPoint;
    public GameObject[] Slugs;
    new bool enabled = true;
    bool inColliderTrigger = false;
    public ParticleSystem spark;
    public ParticleSystem blueFlame;
    public GameObject blueFlare;
    public InputActionProperty pinchAction;
    float triggerValue = 0;
    bool trackIfInUpdate;
    float trackDetalTime = 0;
    public float slugSpeedOffset =0.2f;
    //public GameObject stepSystemAutoObj;
    void Start()
    {
        //stepSystemAuto=stepSystemAutoObj.GetComponent<StepSystemAuto>();
        var interactable = gameObject.GetComponent<XRGrabInteractable>();
        spark.Stop();
        blueFlame.Stop();
        blueFlare.SetActive(false);
    }
    private void Awake()
    {
        enabled = true;
    }

    void FixedUpdate()
    {
        triggerValue =pinchAction.action.ReadValue<float>();
        if(triggerValue > 0 && inColliderTrigger)
        {
          trackDetalTime += Time.deltaTime;
            if (trackDetalTime >slugSpeedOffset)
            {
                trackDetalTime = 0;
                Instantiate(Slugs[Random.Range(0, Slugs.Length)], transform.position, transform.rotation);
            }
           
        }
        if(triggerValue > 0 && inColliderTrigger &&trackIfInUpdate)
        {
           trackIfInUpdate = false;
            spark.Play();
            blueFlame.Play();
            blueFlare.SetActive(true);
        }
        else if(triggerValue == 0 || !inColliderTrigger && !trackIfInUpdate)
        {
            trackIfInUpdate=true;
            spark.Stop();
            blueFlame.Stop();
            blueFlare.SetActive(false);
        }        
    }
//trash Enum

   // IEnumerator ShootSlugPerSecond()
  //  {
        
    //    while (triggerValue > 0 && enabled &&inColliderTrigger)
    //    {
   //         Debug.Log("slugShootPer sec Enum");
   //         enabled = false;
   //         int index = Random.Range(0, Slugs.Length);
   //         Instantiate(Slugs[index], transform.position, transform.rotation);

//
  //          yield return new WaitForSeconds(0.2f);
   //         enabled = true;
   //     }
 //   }
    public void offParticles()
    {
        Debug.Log("off aprticles called");
        spark.Stop();
        blueFlame.Stop();
        blueFlare.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Metal"))
        {
            inColliderTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Metal"))
        {
            inColliderTrigger = false;
        }
    }
   

}
